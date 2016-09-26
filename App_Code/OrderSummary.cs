namespace BusinessLayer
{
    /// <summary>
    ///     Represents summary info of an Order.
    /// </summary>
    public class OrderSummary
    {
        /// <summary>
        ///     Sets a default gst rate.
        /// </summary>
        public OrderSummary()
        {
            GstRate = 0.15;
        }

        /// <summary>
        ///     Gst Rate to use.
        /// </summary>
        public static double GstRate { get; set; }

        /// <summary>
        ///     ID of order being summarised.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        ///     Sum of quantities in all Orderitems for this order
        /// </summary>
        public int TotalQuantity { get; set; }

        /// <summary>
        ///     Sum of prices in all Orderitems for this order
        /// </summary>
        public double SubTotalPrice { get; set; }

        /// <summary>
        ///     GST portion of total price.
        /// </summary>
        public double SubTotalGst
        {
            get { return SubTotalPrice*GstRate; }
        }

        /// <summary>
        ///     Full total price of order.
        /// </summary>
        public double TotalPrice
        {
            get { return SubTotalPrice*GstRate + SubTotalPrice; }
        }

        /// <summary>
        ///     Reference to order being summarised.
        /// </summary>
        public virtual CustomerOrder CustomerOrder { get; set; }
    }
}