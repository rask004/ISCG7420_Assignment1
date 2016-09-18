using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using BusinessLayer;


namespace DataLayer
{

    /// <summary>
    /// DataLayer Tier Management Object
    /// 
    /// Uses a generic pattern for all methods. The Entity Type must be specified for each method.
    /// </summary>
    public class DataManager
    {
        private readonly OleDbConnection _connection;

        private readonly string _buildSiteUserTable = "if OBJECT_ID(N'dbo.SiteUser', N'U') is NULL BEGIN " +
                                                     "create table dbo.SiteUser(id   int   IDENTITY(1, 1)   primary key, login   nvarchar(64)    not null, " +
                                                     "password    nvarchar(64)    not null, userType    char(1)     not null, emailAddress    nvarchar(100)   not null, " +
                                                     "homeNumber  nvarchar(11), workNumber  nvarchar(11), mobileNumber    nvarchar(14), firstName   nvarchar(32), " +
                                                     "lastName    nvarchar(32), streetAddress   nvarchar(64), suburb      nvarchar(24), city        nvarchar(16), " +
                                                     "isDisabled  bit     DEFAULT 0 ); END ";

        private readonly string _buildOrderTable = "if OBJECT_ID(N'dbo.CustomerOrder', N'U') is NULL BEGIN " +
                                                   "create table dbo.CustomerOrder(id int IDENTITY(1, 1)   primary key, userId  int    not null Foreign Key References dbo.SiteUser(id), " +
                                                   "status  nvarchar(7) not null DEFAULT 'waiting', datePlaced datetime not null DEFAULT GETDATE()); END ";

        private readonly string _buildSupplierTable = "if OBJECT_ID(N'dbo.Supplier', N'U') is NULL BEGIN " +
                                                      "create table dbo.Supplier(id int IDENTITY(1, 1)   primary key, name    nvarchar(32)    not null, " +
                                                      "homeNumber   nvarchar(11)    null, worknumber   nvarchar(11)    null, " +
                                                      "mobileNumber nvarchar(13)    null, emailAddress    nvarchar(64)    not null); END ";

        private readonly string _buildCategoryTable = "if OBJECT_ID(N'dbo.Category', N'U') is NULL BEGIN " +
                                                      "create table dbo.Category(id int IDENTITY(1, 1)   primary key, name    nvarchar(40)    not null); END ";

        private readonly string _buildColourTable = "if OBJECT_ID(N'dbo.Colour', N'U') is NULL BEGIN " +
                                                    "create table dbo.Colour(id int IDENTITY(1, 1)   primary key, name    nvarchar(24)    not null); END ";

        private readonly string _buildCapTable = "if OBJECT_ID(N'dbo.Cap', N'U') is NULL BEGIN " +
                                                 "create table dbo.Cap(id int IDENTITY(1, 1)   primary key, name    nvarchar(40)    not null, " +
                                                 "price   real    not null, description nvarchar(512)   not null, imageUrl nvarchar(96) not null, " +
                                                 "supplierId  int     not null    Foreign Key References dbo.Supplier(id), " +
                                                 "categoryId  int     not null    Foreign Key References dbo.Category(id)); END ";

        private readonly string _buildOrderItemTable = "if OBJECT_ID(N'dbo.OrderItem', N'U') is NULL BEGIN " +
                                                       "create table dbo.OrderItem(orderId     int     not null    Foreign Key References dbo.CustomerOrder(id), " +
                                                       "capId       int     not null    Foreign Key References dbo.Cap(id), " +
                                                       "colourId    int     not null    Foreign Key References dbo.Colour(id), " +
                                                       "quantity    int     not null, " +
                                                       "Constraint  orderItem_pk    Primary Key(colourId, capId, orderId)); END ";

        private readonly string _insertDefaultUserAdmin = "if (select count(id) from dbo.SiteUser) = 0 BEGIN " +
                                                          "insert into SiteUser (login, password, userType, emailAddress) Values('AdminRolandAskew2016', " +
                                                          "'BB51AD0AAB66C70D3B26CEC4EFCC224273AF5E18', 'A', 'AskewR04@myunitec.ac.nz'); " +
                                                          "END ";

        private readonly string _insertDefaultColours = "if (select count(id) from dbo.Colour) = 0 BEGIN " +
                                                        "insert into colour (name) values('Black'), ('White'), ('Blue'), ('Green'), ('Red'), ('Pink'), ('Yellow'), ('Orange'), ('Grey'); " +
                                                        "END ";

        private readonly string _insertDefaultCategories = "if (select count(id) from dbo.Category) = 0 BEGIN " +
                                                           "insert into category (name) values('Business Caps'), ('Women''s Caps'), ('Men''s Caps'), ('Children''s Caps'); " +
                                                           "END ";

        private readonly string _insertDefaultSuppliers = "if (select count(id) from dbo.Supplier) = 0 BEGIN " +
                                                           "insert into Supplier (name, homeNumber, emailAddress, workNumber, mobileNumber) " +
                                                           "values('Escobar Fabrics', 'sales@escobar.co.nz', '094443333','',''),('Alto Monte Fashion','sales@altomonte.com','073347776','',''); " +
                                                           "END ";


        private readonly string _selectAllCustomers = "Select * from SiteUser where userType='C';";

        private readonly string _selectAllAdmins = "Select * from SiteUser where userType='A';";

        private readonly string _selectSingleCustomerById = "Select * from SiteUser where userType='C' and id=?;";

        private readonly string _selectSingleCustomerByLogin = "Select * from SiteUser where userType='C' and login=?;";

        private readonly string _selectSingleCustomerByEmail = "Select * from SiteUser where userType='C' and emailAddress=?;";

        private readonly string _selectSingleAdminById = "Select * from SiteUser where userType='A' and id=?;";

        private readonly string _selectSingleAdminByLogin = "Select * from SiteUser where userType='A' and login=?;";

        private readonly string _insertAdministrator =
            "Insert into SiteUser (login, password, userType, emailAddress) values (?, ?, 'A', ?);";

        private readonly string _insertCustomer =
            "Insert into SiteUser (login, password, userType, emailAddress, firstName, lastName, homeNumber, workNumber, mobileNumber, streetAddress, suburb, city, isDisabled) values (?, ?, 'C', ?, ?, ?, ?, ?, ?, ?, ?, ?, ?);";

        private readonly string _updateCustomerNoPassword =
            "Update SiteUser set login=?, userType='C', emailAddress=?, firstName=?, lastName=?, homeNumber=?, workNumber=?, mobileNumber=?, streetAddress=?, suburb=?, city=? where id=?;";

        private readonly string _updateCustomersPassword =
            "Update SiteUser set password=? where id=? and userType='C';";

        private readonly string _updateAdministratorNoPassword =
            "Update SiteUser set login=?, userType='A', emailAddress=? where id=?;";

        private readonly string _updateAdministratorsPassword =
            "Update SiteUser set password=? where id=? and userType='A';";

        private readonly string _updateCustomerIsDisabled = "Update SiteUser set IsDisabled=1 where id=?";

        private readonly string _selectAllCategories = "select * from Category;";

        private readonly string _selectSingleCategoryById = "Select * from Category where id=?;";

        private readonly string _insertCategory = "insert into Category (name) values (?);";

        private readonly string _updateCategory = "update Category set name=? where id=?;";

        private readonly string _selectAllColours = "select * from Colour;";

        private readonly string _selectSingleColourById = "Select * from Colour where id=?;";

        private readonly string _insertColour = "insert into Colour (name) values (?);";

        private readonly string _updateColour = "update Colour set name=? where id=?;";

        private readonly string _insertSupplier = "insert into Supplier (name, homeNumber, workNumber, mobileNumber, emailAddress) values (?, ?, ?, ?, ?);";

        private readonly string _updateSupplier = "update Supplier set name=?, homeNumber=?, workNumber=?, mobileNumber=?, emailAddress=? where id=?;";

        private readonly string _selectAllSuppliers = "select * from Supplier;";

        private readonly string _selectSingleSupplierById = "Select * from Supplier where id=?;";

        private readonly string _selectAllCaps = "select * from Cap;";

        private readonly string _selectSingleCapById = "Select * from Cap where id=?;";

        private readonly string _insertCap = "insert into Cap (name, price, description, imageUrl, categoryId, supplierId) values (?, ?, ?);";

        private readonly string _updateCap = "update Cap set name=?, price=?, description=?, imageUrl=?, categoryId=?, supplierId=? where id=?;";

        private readonly string _updateCapCategoryId = "update Cap set categoryId=? where id=?;";

        private readonly string _updateCapSupplierId = "update Cap set supplierId=? where id=?;";

        private readonly string _selectAllOrders = "select * from CustomerOrder ORDER BY datePlaced DESC;";

        private readonly string _selectSingleOrderById = "Select * from CustomerOrder where id=?;";

        private readonly string _insertOrder = "insert into CustomerOrder (status, userId, datePlaced) values (?, ?, ?);";

        private readonly string _updateOrderStatus = "update CustomerOrder set status=? where id=?;";

        private readonly string _selectAllOrderItemsWithMatchingOrderId = "select * from OrderItem where orderId=?";

        private readonly string _insertOrderItem = "insert into OrderItem (orderId, capId, colourId, quantity) values (?, ?, ?, ?);";

        private readonly string _selectAllCapsByCategoryId = "select * from Cap where categoryId=?;";


        private DataManager()
        {
            _connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["DeveloperExpressConnection"]
                .ConnectionString);

            BuildDatabase();
        }

        /// <summary>
        ///     Queries to build tables and initial state.
        /// </summary>
        private void BuildDatabase()
        {
            var dbCommand = new OleDbCommand(_buildSiteUserTable, _connection);
            RunDbCommandNoResults(dbCommand);

            dbCommand = new OleDbCommand(_buildOrderTable, _connection);
            RunDbCommandNoResults(dbCommand);

            dbCommand = new OleDbCommand(_buildCategoryTable, _connection);
            RunDbCommandNoResults(dbCommand);

            dbCommand = new OleDbCommand(_buildSupplierTable, _connection);
            RunDbCommandNoResults(dbCommand);

            dbCommand = new OleDbCommand(_buildColourTable, _connection);
            RunDbCommandNoResults(dbCommand);

            dbCommand = new OleDbCommand(_buildCapTable, _connection);
            RunDbCommandNoResults(dbCommand);

            dbCommand = new OleDbCommand(_buildOrderItemTable, _connection);
            RunDbCommandNoResults(dbCommand);

            dbCommand = new OleDbCommand(_insertDefaultCategories, _connection);
            RunDbCommandNoResults(dbCommand);

            dbCommand = new OleDbCommand(_insertDefaultColours , _connection);
            RunDbCommandNoResults(dbCommand);

            dbCommand = new OleDbCommand(_insertDefaultUserAdmin, _connection);
            RunDbCommandNoResults(dbCommand);

            dbCommand = new OleDbCommand(_insertDefaultSuppliers, _connection);
            RunDbCommandNoResults(dbCommand);
        }

        /// <summary>
        ///     Run an SQL query without returning a result set.
        /// </summary>
        /// <param name="dbCommand"></param>
        private void RunDbCommandNoResults(OleDbCommand dbCommand)
        {
            try
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                dbCommand.ExecuteNonQuery();
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
                
            }
            
        }

        /// <summary>
        ///     Getter for DataManager Singleton Instance
        /// </summary>
        public static DataManager Instance
        {
            get
            {
                return new DataManager();
            }
        }

        /// <summary>
        ///     OleDb method to get list of all customers.
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetAllCustomers()
        {
            List<Customer> records = new List<Customer>();
            OleDbDataReader reader = null;

            try
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                reader = (new OleDbCommand(_selectAllCustomers, _connection)).ExecuteReader();

                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Customer customer = new Customer();
                        customer.ID = reader.GetInt32(0);
                        customer.FirstName = reader["firstName"].ToString();
                        customer.LastName = reader["lastName"].ToString();
                        customer.Login = reader["login"].ToString();
                        customer.Email = reader["emailAddress"].ToString();
                        customer.HomeNumber = reader["homeNumber"].ToString();
                        customer.WorkNumber = reader["workNumber"].ToString();
                        customer.MobileNumber = reader["mobileNumber"].ToString();
                        customer.StreetAddress = reader["streetAddress"].ToString();
                        customer.Suburb = reader["suburb"].ToString();
                        customer.City = reader["city"].ToString();
                        records.Add(customer);
                    }
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                _connection.Close();
            }

            return records;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void DisableExistingCustomer(int id)
        {
            OleDbCommand command = new OleDbCommand(_updateCustomerIsDisabled, _connection);
            command.Parameters.Add(new OleDbParameter("@IDENTIFIER", OleDbType.Integer));
            command.Parameters["@IDENTIFIER"].Value = id;
            RunDbCommandNoResults(command);
        }

        /// <summary>
        ///     Return a single customer referenced by id. If no customer fetched, return null.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer GetSingleCustomerById(int id)
        {
            OleDbDataReader reader = null;
            Customer customer = null;

            try
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }

                OleDbCommand command = new OleDbCommand(_selectSingleCustomerById, _connection);
                command.Parameters.Add(new OleDbParameter("@IDENTIFIER", OleDbType.Integer));
                command.Parameters["@IDENTIFIER"].Value = id;

                reader = (command.ExecuteReader());


                if (reader != null && reader.HasRows && reader.Read())
                {
                    customer = new Customer();
                    customer.ID = Convert.ToInt32(reader["id"]);
                    customer.Login = reader["login"].ToString();
                    customer.Email = reader["emailAddress"].ToString();
                    customer.Password = reader["password"].ToString();
                    customer.FirstName = reader["firstName"].ToString();
                    customer.LastName = reader["lastName"].ToString();
                    customer.HomeNumber = reader["homeNumber"].ToString();
                    customer.WorkNumber = reader["workNumber"].ToString();
                    customer.MobileNumber = reader["mobileNumber"].ToString();
                    customer.StreetAddress = reader["streetAddress"].ToString();
                    customer.Suburb = reader["suburb"].ToString();
                    customer.City = reader["city"].ToString();
                    customer.IsDisabled = Convert.ToBoolean(reader["isDisabled"]);

                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                _connection.Close();
            }


            return customer;
        }

        /// <summary>
        ///     Return a single customer referenced by login. If no customer fetched, return null.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public Customer GetSingleCustomerByLogin(string login)
        {
            Customer customer = null;
            OleDbDataReader reader = null;

            try
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                OleDbCommand command = new OleDbCommand(_selectSingleCustomerByLogin, _connection);
                command.Parameters.Add(new OleDbParameter("@LOGIN", OleDbType.VarChar));
                command.Parameters["@LOGIN"].Value = login;
                reader = (command.ExecuteReader());
                
                if (reader != null && reader.HasRows && reader.Read())
                {
                    customer = new Customer();
                    customer.ID = Convert.ToInt32(reader["id"]);
                    customer.Login = reader["login"].ToString();
                    customer.Email = reader["emailAddress"].ToString();
                    customer.Password = reader["password"].ToString();
                    customer.FirstName = reader["firstName"].ToString();
                    customer.LastName = reader["lastName"].ToString();
                    customer.HomeNumber = reader["homeNumber"].ToString();
                    customer.WorkNumber = reader["workNumber"].ToString();
                    customer.MobileNumber = reader["mobileNumber"].ToString();
                    customer.StreetAddress = reader["streetAddress"].ToString();
                    customer.Suburb = reader["suburb"].ToString();
                    customer.City = reader["city"].ToString();
                    customer.IsDisabled = Convert.ToBoolean(reader["isDisabled"]);

                }
                
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                _connection.Close();

            }

            return customer;
        }

        /// <summary>
        ///     Return a single customer referenced by email. If no customer fetched, return null.
        /// </summary>
        /// <returns></returns>
        public Customer GetSingleCustomerByEmail(string email)
        {
            OleDbDataReader reader = null;
            Customer customer = null;

            try
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }

                OleDbCommand command = new OleDbCommand(_selectSingleCustomerByEmail, _connection);
                command.Parameters.Add(new OleDbParameter("@EMAIL", OleDbType.VarChar));
                command.Parameters["@EMAIL"].Value = email;
                reader = (command.ExecuteReader());

                if (reader != null && reader.HasRows && reader.Read())
                {
                    customer = new Customer();
                    customer.ID = Convert.ToInt32(reader["id"]);
                    customer.Login = reader["login"].ToString();
                    customer.Email = reader["emailAddress"].ToString();
                    customer.Password = reader["password"].ToString();
                    customer.FirstName = reader["firstName"].ToString();
                    customer.LastName = reader["lastName"].ToString();
                    customer.HomeNumber = reader["homeNumber"].ToString();
                    customer.WorkNumber = reader["workNumber"].ToString();
                    customer.MobileNumber = reader["mobileNumber"].ToString();
                    customer.StreetAddress = reader["streetAddress"].ToString();
                    customer.Suburb = reader["suburb"].ToString();
                    customer.City = reader["city"].ToString();
                    customer.IsDisabled = Convert.ToBoolean(reader["isDisabled"]);

                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                _connection.Close();

            }

            return customer;
        }

        /// <summary>
        ///     Add a new customer with this login, email and data
        ///     Use randomised password for security.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="login"></param>
        /// <param name="passwordHash"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="homeNumber"></param>
        /// <param name="workNumber"></param>
        /// <param name="mobileNumber"></param>
        /// <param name="streetAddress"></param>
        /// <param name="suburb"></param>
        /// <param name="city"></param>
        public void AddNewCustomer(string email, string login, string passwordHash,
            string firstName, string lastName, string homeNumber,
                    string workNumber, string mobileNumber, string streetAddress, string suburb, string city)
        {
            OleDbCommand command = new OleDbCommand(_insertCustomer, _connection);
            command.Parameters.Add(new OleDbParameter("@LOGIN", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@PASSWORD", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@EMAIL", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@FIRSTNAME", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@LASTNAME", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@HOMENUMBER", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@WORKNUMBER", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@MOBILENUMBER", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@STREETADDRESS", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@SUBURB", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@CITY", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@DISABLED", OleDbType.Boolean));
            command.Parameters["@EMAIL"].Value = email;
            command.Parameters["@LOGIN"].Value = login;
            command.Parameters["@PASSWORD"].Value = passwordHash;
            command.Parameters["@FIRSTNAME"].Value = firstName;
            command.Parameters["@LASTNAME"].Value = lastName;
            command.Parameters["@HOMENUMBER"].Value = homeNumber;
            command.Parameters["@WORKNUMBER"].Value = workNumber;
            command.Parameters["@MOBILENUMBER"].Value = mobileNumber;
            command.Parameters["@STREETADDRESS"].Value = streetAddress;
            command.Parameters["@SUBURB"].Value = suburb;
            command.Parameters["@CITY"].Value = city;
            command.Parameters["@DISABLED"].Value = false;

            RunDbCommandNoResults(command);
        }

        /// <summary>
        ///     Update an existing customer by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="email"></param>
        /// <param name="login"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="homeNumber"></param>
        /// <param name="workNumber"></param>
        /// <param name="mobileNumber"></param>
        /// <param name="streetAddress"></param>
        /// <param name="suburb"></param>
        /// <param name="city"></param>
        public void UpdateExistingCustomer(int id, string email, string login,
            string firstName, string lastName, string homeNumber,
                    string workNumber, string mobileNumber, string streetAddress, string suburb, string city)
        {

            OleDbCommand command =
                new OleDbCommand(_updateCustomerNoPassword, _connection);
            command.Parameters.Add(new OleDbParameter("@LOGIN", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@EMAIL", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@FIRSTNAME", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@LASTNAME", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@HOMENUMBER", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@WORKNUMBER", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@MOBILENUMBER", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@STREETADDRESS", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@SUBURB", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@CITY", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@IDENTIFIER", OleDbType.Integer));
            command.Parameters["@EMAIL"].Value = email;
            command.Parameters["@LOGIN"].Value = login;
            command.Parameters["@IDENTIFIER"].Value = id;
            command.Parameters["@FIRSTNAME"].Value = firstName;
            command.Parameters["@LASTNAME"].Value = lastName;
            command.Parameters["@HOMENUMBER"].Value = homeNumber;
            command.Parameters["@WORKNUMBER"].Value = workNumber;
            command.Parameters["@MOBILENUMBER"].Value = mobileNumber;
            command.Parameters["@STREETADDRESS"].Value = streetAddress;
            command.Parameters["@SUBURB"].Value = suburb;
            command.Parameters["@CITY"].Value = city;
            RunDbCommandNoResults(command);

            
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="id"></param>
        /// <param name="passwordHash"></param>
        public void UpdateExistingCustomerPassword(int id, string passwordHash)
        {
            OleDbCommand command =
                new OleDbCommand(_updateCustomersPassword,
                    _connection);
            command.Parameters.Add(new OleDbParameter("@PASSWORD", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@IDENTIFIER", OleDbType.Integer));
            command.Parameters["@IDENTIFIER"].Value = id;
            command.Parameters["@PASSWORD"].Value = passwordHash;
            RunDbCommandNoResults(command);
        }

        /// <summary>
        ///     OleDb method to get list of all admins.
        /// </summary>
        /// <returns></returns>
        public List<Administrator> GetAllAdministrators()
        {
            List<Administrator> records = new List<Administrator>();
            OleDbDataReader reader = null;

            try
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                reader = (new OleDbCommand(_selectAllAdmins, _connection)).ExecuteReader();

                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Administrator admin = new Administrator();
                        admin.ID = Convert.ToInt32(reader["id"]);
                        admin.Login = reader["login"].ToString();
                        admin.Email = reader["emailAddress"].ToString();
                        admin.Password = reader["password"].ToString();
                        records.Add(admin);

                    }
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                _connection.Close();
            }

            

            return records;
        }

        /// <summary>
        ///     Return a single customer referenced by id. If no customer fetched, return null.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Administrator GetSingleAdministratorById(int id)
        {
            OleDbDataReader reader = null;
            Administrator record = null;

            try
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                OleDbCommand command = new OleDbCommand(_selectSingleAdminById, _connection);
                command.Parameters.Add(new OleDbParameter("@IDENTIFIER", OleDbType.Integer));
                command.Parameters["@IDENTIFIER"].Value = id;
                reader = (command.ExecuteReader());


                if (reader != null && reader.HasRows && reader.Read())
                {
                    record = new Administrator();
                    record.ID = Convert.ToInt32(reader["id"]);
                    record.Login = reader["login"].ToString();
                    record.Email = reader["emailAddress"].ToString();
                    record.Password = reader["password"].ToString();

                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                _connection.Close();
            }

            return record;
        }

        /// <summary>
        ///     Return a single admin referenced by login. If no admin fetched, return null.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public Administrator GetSingleAdministratorByLogin(string login)
        {
            OleDbDataReader reader = null;
            Administrator record = null;

            try
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                OleDbCommand command = new OleDbCommand(_selectSingleAdminByLogin, _connection);
                command.Parameters.Add(new OleDbParameter("@LOGIN", OleDbType.VarChar));
                command.Parameters["@LOGIN"].Value = login;
                reader = (command.ExecuteReader());

                if (reader != null && reader.HasRows && reader.Read())
                {
                    record = new Administrator();
                    record.ID = Convert.ToInt32(reader["id"]);
                    record.Login = reader["login"].ToString();
                    record.Email = reader["emailAddress"].ToString();
                    record.Password = reader["password"].ToString();

                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                _connection.Close();
            }
            
            return record;
        }


        /// <summary>
        ///     Add a new admin with this login  and email
        ///     Use randomised password for security.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="login"></param>
        /// <param name="passwordHash"></param>
        public void AddNewAdmin(string email, string login, string passwordHash)
        {
            OleDbCommand command = new OleDbCommand(_insertAdministrator, _connection);
            command.Parameters.Add(new OleDbParameter("@LOGIN", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@PASSWORD", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@EMAIL", OleDbType.VarChar));
            command.Parameters["@LOGIN"].Value = login;
            command.Parameters["@EMAIL"].Value = email;
            command.Parameters["@PASSWORD"].Value = passwordHash;
            RunDbCommandNoResults(command);
        }

        /// <summary>
        ///     Update an existing admin by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="email"></param>
        /// <param name="login"></param>
        public void UpdateExistingAdmin(int id, string email, string login)
        {
            OleDbCommand command =
                new OleDbCommand(_updateAdministratorNoPassword,
                    _connection);
            command.Parameters.Add(new OleDbParameter("@LOGIN", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@EMAIL", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@IDENTIFIER", OleDbType.Integer));
            command.Parameters["@IDENTIFIER"].Value = id;
            command.Parameters["@EMAIL"].Value = email;
            command.Parameters["@LOGIN"].Value = login;

            RunDbCommandNoResults(command);
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="id"></param>
        /// <param name="passwordHash"></param>
        public void UpdateExistingAdminPassword(int id, string passwordHash)
        {
            OleDbCommand command =
                new OleDbCommand(_updateAdministratorsPassword,
                    _connection);
            command.Parameters.Add(new OleDbParameter("@PASSWORD", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@IDENTIFIER", OleDbType.Integer));
            command.Parameters["@PASSWORD"].Value = passwordHash;
            command.Parameters["@IDENTIFIER"].Value = id;

            RunDbCommandNoResults(command);
        }


        /// <summary>
        ///     get list of all Category.
        /// </summary>
        /// <returns></returns>
        public List<Category> GetAllCategories()
        {
            List<Category> records = new List<Category>();
            OleDbDataReader reader = null;

            try
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                reader = (new OleDbCommand(_selectAllCategories, _connection)).ExecuteReader();

                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Category category = new Category();
                        category.ID = Convert.ToInt32(reader["id"]);
                        category.Name = reader["name"].ToString();
                        records.Add(category);
                    }
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                _connection.Close();
            }

            return records;
        }

        /// <summary>
        ///     Return a single Category referenced by id. If no Category fetched, return null.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Category GetSingleCategoryById(int id)
        {
            OleDbDataReader reader = null;
            Category category = null;

            try
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }

                OleDbCommand command = new OleDbCommand(_selectSingleCategoryById, _connection);
                command.Parameters.Add(new OleDbParameter("@IDENTIFIER", OleDbType.Integer));
                command.Parameters["@IDENTIFIER"].Value = id;
                reader = (command.ExecuteReader());


                if (reader != null && reader.HasRows && reader.Read())
                {
                    category = new Category();
                    category.ID = Convert.ToInt32(reader["id"]);
                    category.Name = reader["name"].ToString();
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                _connection.Close();
            }


            return category;
        }

        /// <summary>
        ///     Add a new Category with this name
        /// </summary>
        /// <param name="name"></param>
        public void AddNewCategory(string name)
        {
            OleDbCommand command = new OleDbCommand(_insertCategory, _connection);
            command.Parameters.Add(new OleDbParameter("@NAME", OleDbType.VarChar));
            command.Parameters["@NAME"].Value = name;
            RunDbCommandNoResults(command);
        }

        /// <summary>
        ///     Update an existing Category by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public void UpdateExistingCategory(int id, string name)
        {
            OleDbCommand command =
                new OleDbCommand(_updateCategory,
                    _connection);
            command.Parameters.Add(new OleDbParameter("@NAME", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@IDENTIFIER", OleDbType.Integer));
            command.Parameters["@IDENTIFIER"].Value = id;
            command.Parameters["@NAME"].Value = name;

            RunDbCommandNoResults(command);
        }



        /// <summary>
        ///     get list of all Colour.
        /// </summary>
        /// <returns></returns>
        public List<Colour> GetAllColours()
        {
            List<Colour> records = new List<Colour>();
            OleDbDataReader reader = null;

            try
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }

                reader = (new OleDbCommand(_selectAllColours, _connection)).ExecuteReader();

                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Colour colour = new Colour();
                        colour.ID = Convert.ToInt32(reader["id"]);
                        colour.Name = reader["name"].ToString();
                        records.Add(colour);
                    }
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                _connection.Close();
            }

            return records;
        }

        /// <summary>
        ///     Return a single Colour referenced by id. If no Colour fetched, return null.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Colour GetSingleColourById(int id)
        {
            OleDbDataReader reader = null;
            Colour colour = null;

            try
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }

                OleDbCommand command = new OleDbCommand(_selectSingleColourById, _connection);
                command.Parameters.Add(new OleDbParameter("@IDENTIFIER", OleDbType.Integer));
                command.Parameters["@IDENTIFIER"].Value = id;
                reader = (command.ExecuteReader());


                if (reader != null && reader.HasRows && reader.Read())
                {
                    colour = new Colour();
                    colour.ID = Convert.ToInt32(reader["id"]);
                    colour.Name = reader["name"].ToString();
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                _connection.Close();
            }


            return colour;
        }

        /// <summary>
        ///     Add a new Colour with this name
        /// </summary>
        /// <param name="name"></param>
        public void AddNewColour(string name)
        {
            OleDbCommand command = new OleDbCommand(_insertColour, _connection);
            command.Parameters.Add(new OleDbParameter("@NAME", OleDbType.VarChar));
            command.Parameters["@NAME"].Value = name;
            RunDbCommandNoResults(command);
        }

        /// <summary>
        ///     Update an existing Colour by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public void UpdateExistingColour(int id, string name)
        {
            OleDbCommand command =
                new OleDbCommand(_updateColour,
                    _connection);
            command.Parameters.Add(new OleDbParameter("@NAME", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@IDENTIFIER", OleDbType.Integer));
            command.Parameters["@IDENTIFIER"].Value = id;
            command.Parameters["@NAME"].Value = name;

            RunDbCommandNoResults(command);
        }


        /// <summary>
        ///     get list of all suppliers.
        /// </summary>
        /// <returns></returns>
        public List<Supplier> GetAllSuppliers()
        {
            List<Supplier> records = new List<Supplier>();
            OleDbDataReader reader = null;

            try
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }

                reader = (new OleDbCommand(_selectAllSuppliers, _connection)).ExecuteReader();

                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Supplier item = new Supplier();
                        item.ID = Convert.ToInt32(reader["id"]);
                        item.Name = reader["name"].ToString();
                        item.Email = reader["emailAddress"].ToString();
                        item.HomeNumber = reader["homeNumber"].ToString();
                        item.WorkNumber = reader["workNumber"].ToString();
                        item.MobileNumber = reader["mobileNumber"].ToString();
                        records.Add(item);
                    }
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                _connection.Close();
            }

            return records;
        }

        /// <summary>
        ///     Return a single supplier referenced by id. If no supplier fetched, return null.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Supplier GetSingleSupplierById(int id)
        {
            OleDbDataReader reader = null;
            Supplier item = null;

            try
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }

                OleDbCommand command = new OleDbCommand(_selectSingleSupplierById, _connection);
                command.Parameters.Add(new OleDbParameter("@IDENTIFIER", OleDbType.Integer));
                command.Parameters["@IDENTIFIER"].Value = id;
                reader = (command.ExecuteReader());


                if (reader != null && reader.HasRows && reader.Read())
                {
                    item = new Supplier();
                    item.ID = Convert.ToInt32(reader["id"]);
                    item.Name = reader["name"].ToString();
                    item.Email = reader["emailAddress"].ToString();
                    item.HomeNumber = reader["homeNumber"].ToString();
                    item.WorkNumber = reader["workNumber"].ToString();
                    item.MobileNumber = reader["mobileNumber"].ToString();
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                _connection.Close();
            }


            return item;
        }

        /// <summary>
        ///     Add a new supplier with this name, contact number  and email
        /// </summary>
        /// <param name="name"></param>
        /// <param name="homeNumber"></param>
        /// <param name="workNumber"></param>
        /// <param name="mobileNumber"></param>
        /// <param name="email"></param>
        public void AddNewSupplier(string name, string homeNumber, string workNumber, string mobileNumber, string email)
        {
            OleDbCommand command = new OleDbCommand(_insertSupplier, _connection);
            command.Parameters.Add(new OleDbParameter("@NAME", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@HOMENUMBER", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@WORKNUMBER", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@MOBILENUMBER", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@EMAIL", OleDbType.VarChar));
            command.Parameters["@NAME"].Value = name;
            command.Parameters["@HOMENUMBER"].Value = homeNumber;
            command.Parameters["@WORKNUMBER"].Value = workNumber;
            command.Parameters["@MOBILENUMBER"].Value = mobileNumber;
            command.Parameters["@EMAIL"].Value = email;
            RunDbCommandNoResults(command);
        }


        /// <summary>
        ///     Update an existing supplier by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="homeNumber"></param>
        /// <param name="workNumber"></param>
        /// <param name="mobileNumber"></param>
        /// <param name="email"></param>
        public void UpdateExistingSupplier(int id, string name, string homeNumber, string workNumber, string mobileNumber, string email)
        {
            OleDbCommand command =
                new OleDbCommand(_updateSupplier,
                    _connection);
            command.Parameters.Add(new OleDbParameter("@NAME", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@HOMENUMBER", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@WORKNUMBER", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@MOBILENUMBER", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@EMAIL", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@IDENTIFIER", OleDbType.Integer));
            command.Parameters["@NAME"].Value = name;
            command.Parameters["@HOMENUMBER"].Value = homeNumber;
            command.Parameters["@WORKNUMBER"].Value = workNumber;
            command.Parameters["@MOBILENUMBER"].Value = mobileNumber;
            command.Parameters["@EMAIL"].Value = email;
            command.Parameters["@IDENTIFIER"].Value = id;

            RunDbCommandNoResults(command);
        }


        /// <summary>
        ///     get list of all cap.
        /// </summary>
        /// <returns></returns>
        public List<Cap> GetAllCaps()
        {
            List<Cap> records = new List<Cap>();
            OleDbDataReader reader = null;

            try
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }

                reader = (new OleDbCommand(_selectAllCaps, _connection)).ExecuteReader();

                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Cap item = new Cap();
                        item.ID = Convert.ToInt32(reader["id"]);
                        item.Name = reader["name"].ToString();
                        item.Price = Convert.ToSingle(reader["price"]);
                        item.Description = reader["description"].ToString();
                        item.ImageUrl = reader["imageUrl"].ToString();
                        item.CategoryId = Convert.ToInt32(reader["categoryId"]);
                        item.SupplierId = Convert.ToInt32(reader["supplierId"]);
                        records.Add(item);
                    }

                    // These may close the connection after finding the relevant object.
                    // As DataReader requires an open connection, finish using the DataReader before using these methods.
                    foreach (Cap cap in records)
                    {
                        cap.Category = GetSingleCategoryById(cap.CategoryId);
                        cap.Supplier = GetSingleSupplierById(cap.SupplierId);
                    }
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                _connection.Close();
            }

            return records;
        }


        /// <summary>
        ///     Return a single cap referenced by id. If no cap fetched, return null.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Cap GetSingleCapById(int id)
        {
            OleDbDataReader reader = null;
            Cap item = null;

            try
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }

                OleDbCommand command = new OleDbCommand(_selectSingleCapById, _connection);
                command.Parameters.Add(new OleDbParameter("@IDENTIFIER", OleDbType.Integer));
                command.Parameters["@IDENTIFIER"].Value = id;
                reader = (command.ExecuteReader());


                if (reader != null && reader.HasRows && reader.Read())
                {
                    item = new Cap();
                    item.ID = Convert.ToInt32(reader["id"]);
                    item.Name = reader["name"].ToString();
                    item.Price = Convert.ToSingle(reader["price"]);
                    item.Description = reader["description"].ToString();
                    item.ImageUrl = reader["imageUrl"].ToString();
                    item.CategoryId = Convert.ToInt32(reader["categoryId"]);
                    item.SupplierId = Convert.ToInt32(reader["supplierId"]);
                }

                // These may close the connection after finding the relevant object.
                // As DataReader requires an open connection, finish using the DataReader before using these methods.
                if (item != null)
                {
                    item.Category = GetSingleCategoryById(item.CategoryId);
                    item.Supplier = GetSingleSupplierById(item.SupplierId);
                }
                
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                _connection.Close();
            }

            return item;
        }


        /// <summary>
        ///     Add a new cap with this name and data.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <param name="description"></param>
        /// <param name="imageUrl"></param>
        /// <param name="categoryId"></param>
        /// <param name="supplierId"></param>
        public void AddNewCap(string name, Single price, string description, string imageUrl, int categoryId, int supplierId)
        {
            OleDbCommand command = new OleDbCommand(_insertCap, _connection);
            command.Parameters.Add(new OleDbParameter("@NAME", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@PRICE", OleDbType.Double));
            command.Parameters.Add(new OleDbParameter("@DESCRIPTION", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@IMAGEURL", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@CATEGORYID", OleDbType.Integer));
            command.Parameters.Add(new OleDbParameter("@SUPPLIERID", OleDbType.Integer));
            command.Parameters["@NAME"].Value = name;
            command.Parameters["@PRICE"].Value = price;
            command.Parameters["@DESCRIPTION"].Value = description;
            command.Parameters["@IMAGEURL"].Value = imageUrl;
            command.Parameters["@CATEGORYID"].Value = categoryId;
            command.Parameters["@SUPPLIERID"].Value = supplierId;
            RunDbCommandNoResults(command);
        }


        /// <summary>
        ///     Update an existing cap by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <param name="description"></param>
        /// <param name="imageUrl"></param>
        /// <param name="categoryId"></param>
        /// <param name="supplierId"></param>
        public void UpdateExistingCap(int id, string name, Single price, string description, string imageUrl, int categoryId, int supplierId)
        {
            OleDbCommand command = new OleDbCommand(_updateCap, _connection);
            command.Parameters.Add(new OleDbParameter("@NAME", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@PRICE", OleDbType.Double));
            command.Parameters.Add(new OleDbParameter("@DESCRIPTION", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@IMAGEURL", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@CATEGORYID", OleDbType.Integer));
            command.Parameters.Add(new OleDbParameter("@SUPPLIERID", OleDbType.Integer));
            command.Parameters.Add(new OleDbParameter("@IDENTIFIER", OleDbType.Integer));
            command.Parameters["@NAME"].Value = name;
            command.Parameters["@PRICE"].Value = price;
            command.Parameters["@DESCRIPTION"].Value = description;
            command.Parameters["@IMAGEURL"].Value = imageUrl;
            command.Parameters["@CATEGORYID"].Value = categoryId;
            command.Parameters["@SUPPLIERID"].Value = supplierId;
            command.Parameters["@IDENTIFIER"].Value = id;
            RunDbCommandNoResults(command);
        }

        /// <summary>
        ///     Update an existing cap by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="supplierId"></param>
        public void UpdateExistingCapSupplierId(int id, int supplierId)
        {
            OleDbCommand command = new OleDbCommand(_updateCapSupplierId, _connection);
            command.Parameters.Add(new OleDbParameter("@IDENTIFIER", OleDbType.Integer));
            command.Parameters.Add(new OleDbParameter("@SUPPLIERID", OleDbType.Integer));
            command.Parameters["@IDENTIFIER"].Value = id;
            command.Parameters["@SUPPLIERID"].Value = supplierId;
            RunDbCommandNoResults(command);
        }

        /// <summary>
        ///     Update an existing cap by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoryId"></param>
        public void UpdateExistingCapCategoryId(int id, int categoryId)
        {
            OleDbCommand command = new OleDbCommand(_updateCapCategoryId, _connection);
            command.Parameters.Add(new OleDbParameter("@IDENTIFIER", OleDbType.Integer));
            command.Parameters.Add(new OleDbParameter("@CATEGORYID", OleDbType.Integer));
            command.Parameters["@IDENTIFIER"].Value = id;
            command.Parameters["@CATEGORYID"].Value = categoryId;
            RunDbCommandNoResults(command);
        }

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        public List<CustomerOrder> GetAllOrders()
        {
            List<CustomerOrder> records = new List<CustomerOrder>();
            OleDbDataReader reader = null;

            try
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }

                reader = (new OleDbCommand(_selectAllOrders, _connection)).ExecuteReader();

                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CustomerOrder item = new CustomerOrder();
                        item.ID = Convert.ToInt32(reader["id"]);
                        item.Status = reader["status"].ToString();
                        if (reader["datePlaced"] != DBNull.Value)
                        {
                            item.DatePlaced = Convert.ToDateTime(reader["datePlaced"]);
                        }
                        item.UserId = Convert.ToInt32(reader["userId"]);
                        records.Add(item);
                    }

                    // These may close the connection after finding the relevant object.
                    // As DataReader requires an open connection, finish using the DataReader before using these methods.
                    foreach (var customerOrder in records)
                    {
                        customerOrder.Customer = GetSingleCustomerById(customerOrder.UserId);
                    }
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                _connection.Close();
            }

            return records;
        }


        /// <summary>
        ///     
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CustomerOrder GetSingleOrderById(int id)
        {
            OleDbDataReader reader = null;
            CustomerOrder item = null;

            try
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }

                OleDbCommand command = new OleDbCommand(_selectSingleOrderById, _connection);
                command.Parameters.Add(new OleDbParameter("@IDENTIFIER", OleDbType.Integer));
                command.Parameters["@IDENTIFIER"].Value = id;
                reader = (command.ExecuteReader());


                if (reader != null && reader.HasRows && reader.Read())
                {
                    item = new CustomerOrder();
                    item.ID = Convert.ToInt32(reader["id"]);
                    item.Status = reader["status"].ToString();
                    item.UserId = Convert.ToInt32(reader["userId"]);
                    if (reader["datePlaced"] != DBNull.Value)
                    {
                        item.DatePlaced = Convert.ToDateTime(reader["datePlaced"]);
                    } 
                    item.Customer = GetSingleCustomerById(item.UserId);
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                _connection.Close();
            }

            return item;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        public void UpdateOrderStatus(int id, string status)
        {
            OleDbCommand command = new OleDbCommand(_updateOrderStatus, _connection);
            command.Parameters.Add(new OleDbParameter("@STATUS", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@IDENTIFIER", OleDbType.Integer));
            command.Parameters["@STATUS"].Value = status;
            command.Parameters["@IDENTIFIER"].Value = id;
            RunDbCommandNoResults(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="customerId"></param>
        public void InsertNewOrder(string status, int customerId)
        {
            OleDbCommand command = new OleDbCommand(_insertOrder, _connection);
            command.Parameters.Add(new OleDbParameter("@STATUS", OleDbType.VarChar));
            command.Parameters.Add(new OleDbParameter("@CUSTOMERID", OleDbType.Integer));
            command.Parameters.Add(new OleDbParameter("@DATEPLACED", OleDbType.Date));
            command.Parameters["@STATUS"].Value = status;
            command.Parameters["@CUSTOMERID"].Value = customerId;
            command.Parameters["@DATEPLACED"].Value = DateTime.Now;
            RunDbCommandNoResults(command);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public List<OrderItem> GetAllOrderItemsByOrderId(int orderId)
        {
            List<OrderItem> records = new List<OrderItem>();
            OleDbDataReader reader = null;

            // As each order item references a cap and colour, keep a list of the caps and colours retrieved
            // then if separate orderItems reference the same Cap or colour, reuse that cap or colour
            // more efficient for time and memory.
            Dictionary<int, Cap> foundCaps = new Dictionary<int, Cap>();
            Dictionary<int, Colour> foundColours = new Dictionary<int, Colour>();

            OleDbCommand command = new OleDbCommand(_selectAllOrderItemsWithMatchingOrderId, _connection);
            command.Parameters.Add(new OleDbParameter("@ORDERID", OleDbType.Integer));
            command.Parameters["@ORDERID"].Value = orderId;

            try
            {
                CustomerOrder order = GetSingleOrderById(orderId);

                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }

                reader = (command).ExecuteReader();

                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderItem item = new OrderItem();
                        item.OrderId = Convert.ToInt32(reader["orderId"]);
                        item.CapId = Convert.ToInt32(reader["capId"]);
                        item.ColourId = Convert.ToInt32(reader["colourId"]);
                        item.Quantity = Convert.ToInt32(reader["quantity"]);
                        item.CustomerOrder = order;
                        records.Add(item);
                    }

                    // make sure if repeat caps / colours are retrieved, they are reused.
                    // so OrderItems with same cap / colour will reference that same Cap / colour object.
                    foreach (var orderItem in records)
                    {
                        if (foundCaps.ContainsKey(orderItem.CapId))
                        {
                            orderItem.Cap = foundCaps[orderItem.CapId];
                        }
                        else
                        {
                            Cap cap = GetSingleCapById(orderItem.CapId);
                            orderItem.Cap = cap;
                            foundCaps[orderItem.CapId] = cap;
                        }

                        if (foundColours.ContainsKey(orderItem.ColourId))
                        {
                            orderItem.Colour = foundColours[orderItem.ColourId];
                        }
                        else
                        {
                            Colour colour = GetSingleColourById(orderItem.ColourId);
                            orderItem.Colour = colour;
                            foundColours[orderItem.ColourId] = colour;
                        }
                    }

                    // Suppliers and categories will still be duplicated, until this is fixed.
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                _connection.Close();
            }

            return records;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="capId"></param>
        /// <param name="colourId"></param>
        /// <param name="quantity"></param>
        public void InsertNewOrderItem(int orderId, int capId, int colourId, int quantity)
        {
            OleDbCommand command = new OleDbCommand(_insertOrderItem, _connection);
            command.Parameters.Add(new OleDbParameter("@ORDERID", OleDbType.Integer));
            command.Parameters.Add(new OleDbParameter("@CAPID", OleDbType.Integer));
            command.Parameters.Add(new OleDbParameter("@COLOURID", OleDbType.Integer));
            command.Parameters.Add(new OleDbParameter("@QUANTITY", OleDbType.Integer));
            command.Parameters["@ORDERID"].Value = orderId;
            command.Parameters["@CAPID"].Value = capId;
            command.Parameters["@COLOURID"].Value = colourId;
            command.Parameters["@QUANTITY"].Value = quantity;
            RunDbCommandNoResults(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public List<Cap> GetCapsByCategoryId(int categoryId)
        {
            Category category = GetSingleCategoryById(categoryId);
            List<Cap> records = new List<Cap>();
            OleDbDataReader reader = null;

            Dictionary< int, Supplier> foundSuppliers = new Dictionary<int, Supplier>();

            OleDbCommand command = new OleDbCommand(_selectAllCapsByCategoryId, _connection);
            command.Parameters.Add(new OleDbParameter("@CATEGORYID", OleDbType.Integer));
            command.Parameters["@CATEGORYID"].Value = categoryId;

            try
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }

                reader = (command).ExecuteReader();

                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Cap item = new Cap();
                        item.ID = Convert.ToInt32(reader["id"]);
                        item.Name = reader["name"].ToString();
                        item.Price = Convert.ToSingle(reader["price"]);
                        item.Description = reader["description"].ToString();
                        item.ImageUrl = reader["imageUrl"].ToString();
                        item.CategoryId = Convert.ToInt32(reader["categoryId"]);
                        item.SupplierId = Convert.ToInt32(reader["supplierId"]);
                        records.Add(item);
                    }

                    // These may close the connection after finding the relevant object.
                    // As DataReader requires an open connection, finish using the DataReader before using these methods.
                    foreach (Cap cap in records)
                    {
                        cap.Category = category;
                        if (foundSuppliers.ContainsKey(cap.SupplierId))
                        {
                            cap.Supplier = foundSuppliers[cap.SupplierId];
                        }
                        else
                        {
                            cap.Supplier = GetSingleSupplierById(cap.SupplierId);
                            foundSuppliers[cap.SupplierId] = cap.Supplier;
                        }
                    }
                    
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                _connection.Close();
            }

            return records;
        }
    }
}