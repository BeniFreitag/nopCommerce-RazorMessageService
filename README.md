nopCommerce Razor MessageService
=========

This Plugin extends the core nopCommerce Message Template Engine with Razor-Support (by [RazorEngine]).

Extend any nopCommerce Message Template with the full power of Razor. It works as a Post-Processor of the Token-Engine, so nothing must be changed. Token will continue to work.

Example of "OrderPlaced.CustomerNotification"

```razor
Hello %Order.CustomerFullName%, 
Thanks for buying from %Store.Name%. Below is the summary of the order. 
...
@{
	if (Model.Order.PaymentMethodSystemName == "Payments.CheckMoneyOrder")
	{
		<text>We will send you an Invoice ...</text>
	}
	else if (Model.Order.PaymentMethodSystemName == "Payments.PayPalStandard")
	{
		<text>You ordered with PayPal. ToDo: Add payment notes here</text>
	}
}
...
Order Number: %Order.OrderNumber%
```

Any Message Template has the related Objects available in the Model. E.g. Model.Order, Model.Customer, Model.Vendor etc.  Works with Subject and Body.


Known Issues
-------
##### TinyMCE Editor removes <text> Tags and Linebreaks
###### Workaround:
Disable TinyMCE Editor for Message Templates by adapting "/Administration/Views/Shared/EditorTemplates/RichEditor.cshtml"
```javascript
if (document.location.pathname.match(/Admin\/MessageTemplate\/Edit/)) return;

tinyMCE.init({
    ...
    extended_valid_elements: "text",
    forced_root_blocks: false
});
```


Supported Message Templates (from IWorkflowMessageService)
----

Template | Available Objects/Properties in @Model. | Notes
--- | --- | ---
Blog.BlogComment | Store, BlogComment
Customer.BackInStock | Store, Customer, BackInStockSubscription
Customer.EmailValidationMessage | Store, Customer
Customer.NewOrderNote | Store, OrderNote, Order, Customer
Customer.NewPM | Store, PrivateMessage
Customer.PasswordRecovery | Store, Customer
Customer.WelcomeMessage | Store, Customer
Forums.NewForumPost | Store, Customer, ForumPost, ForumTopic, Forum
Forums.NewForumTopic | Store, ForumTopic, Forum
GiftCard.Notification | Store, GiftCard
NewCustomer.Notification | Store, Customer
NewReturnRequest.StoreOwnerNotification | Store, Customer, ReturnRequest, OrderItem
News.NewsComment | Store, NewsComment
NewsLetterSubscription.ActivationMessage | Store, Subscription
NewVATSubmitted.StoreOwnerNotification | Store, Customer, VatName, VatAddress
OrderCancelled.CustomerNotification | Store, Order, Customer
OrderCompleted.CustomerNotification | Store, Order, Customer
OrderPaid.CustomerNotification | Store, Order, Customer | **Additional Message Template**
OrderPaid.StoreOwnerNotification | Store, Order, Customer
OrderPlaced.CustomerNotification | Store, Order, Customer
OrderPlaced.StoreOwnerNotification | Store, Order, Customer
OrderPlaced.VendorNotification | Store, Order, Customer, Vendor
Product.ProductReview | Store, ProductReview
QuantityBelow.StoreOwnerNotification | Store, Product
RecurringPaymentCancelled.StoreOwnerNotification | Store, Order, Customer, RecurringPayment
ReturnRequestStatusChanged.CustomerNotification | Store, Customer, ReturnRequest, OrderItem
Service.EmailAFriend | Store, Customer, Product, PersonalMessage, CustomerEmail
ShipmentDelivered.CustomerNotification | Store, Shipment, Order, Customer
ShipmentSent.CustomerNotification | Store, Shipment, Order, Customer
Wishlist.EmailAFriend | Store, Customer, PersonalMessage, CustomerEmail


For the Domain Models of Customer, Store, Product, OrderItem etc. see the API:  https://nopcommerce.codeplex.com/SourceControl/latest#src/Libraries/Nop.Core/Domain/Customers/Customer.cs





How to Install
----
1. Download and extract to the *Plugins* Folder of your Web Application
2. Restart the nopCommerce Application
3. Install the Plugin on the Admin Site at */Admin/Plugin/List*
4. Done



Version
----
###1.11
* "OrderPaid.CustomerNotification" is now sent in the Language the Order was made

###1.10
* Added new Message Template "OrderPaid.CustomerNotification"
* Works with nopCommerce 3.40 

###1.00
* Works with nopCommerce 3.40

[RazorEngine]:https://github.com/Antaris/RazorEngine
