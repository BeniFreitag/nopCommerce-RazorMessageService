using Nop.Core.Domain.Blogs;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Forums;
using Nop.Core.Domain.Messages;
using Nop.Core.Domain.News;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Shipping;
using Nop.Core.Domain.Stores;
using Nop.Core.Domain.Vendors;

namespace ToSic.Nop.Plugins.RazorMessageService
{
	/// <summary>
	/// Model used to generate DummyMessage
	/// </summary>
	/// <remarks>Must be public so it can be accessed by RazorEngine!</remarks>
	public class DummyMessageModel
	{
		public Store Store { get; set; } = new Store();
		public BlogComment BlogComment { get; set; } = new BlogComment();
		public Customer Customer { get; set; } = new Customer();
		public BackInStockSubscription BackInStockSubscription { get; set; } = new BackInStockSubscription();
		public OrderNote OrderNote { get; set; } = new OrderNote();
		public Order Order { get; set; } = new Order();
		public PrivateMessage PrivateMessage { get; set; } = new PrivateMessage();
		public ForumPost ForumPost { get; set; } = new ForumPost();
		public ForumTopic ForumTopic { get; set; } = new ForumTopic();
		public Forum Forum { get; set; } = new Forum();
		public GiftCard GiftCard { get; set; } = new GiftCard();
		public ReturnRequest ReturnRequest { get; set; } = new ReturnRequest();
		public OrderItem OrderItem { get; set; } = new OrderItem();
		public NewsComment NewsComment { get; set; } = new NewsComment();
		public NewsLetterSubscription Subscription { get; set; } = new NewsLetterSubscription();
		public string VatName { get; set; } = string.Empty;
		public string VatAddress { get; set; } = string.Empty;
		public Vendor Vendor { get; set; } = new Vendor();
		public decimal RefundedAmount { get; set; } = 0m;
		public ProductReview ProductReview { get; set; } = new ProductReview();
		public Product Product { get; set; } = new Product();
		public RecurringPayment RecurringPayment { get; set; } = new RecurringPayment();
		public string PersonalMessage { get; set; } = string.Empty;
		public string CustomerEmail { get; set; } = string.Empty;
		public Shipment Shipment { get; set; } = new Shipment();
	}
}
