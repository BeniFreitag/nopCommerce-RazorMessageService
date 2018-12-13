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
...
@if (Nop.Core.Domain.Customers.CustomerExtensions.IsInCustomerRole(Model.Customer, "Registered"))
{
	<div>
		Thanks for your Registration.
	</div>
}
```

Any Message Template has the related Objects available in the Model. E.g. Model.Order, Model.Customer, Model.Vendor etc.  Works with Subject and Body.


Known Issues
-------
##### TinyMCE Editor removes <text> Tags and Linebreaks
###### Workaround:
Disable TinyMCE Editor for Message Templates by adapting "/Administration/Views/Shared/EditorTemplates/RichEditor.cshtml"
```javascript
if (document.location.pathname.match(/Admin\/MessageTemplate\/Edit/)) return;

tinyMCE.init(...;
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
NewsLetterSubscription.DeactivationMessage | Store, Subscription
NewVATSubmitted.StoreOwnerNotification | Store, Customer, VatName, VatAddress
OrderCancelled.CustomerNotification | Store, Order, Customer
OrderCompleted.CustomerNotification | Store, Order, Customer
OrderPaid.CustomerNotification | Store, Order, Customer | **Additional Message Template in 3.40**
OrderPaid.StoreOwnerNotification | Store, Order, Customer
OrderPlaced.CustomerNotification | Store, Order, Customer
OrderPlaced.StoreOwnerNotification | Store, Order, Customer
OrderPlaced.VendorNotification | Store, Order, Customer, Vendor
OrderRefunded.CustomerNotification | Store, Order, Customer, RefundedAmount
OrderRefunded.StoreOwnerNotification | Store, Order, Customer, RefundedAmount
Product.ProductReview | Store, ProductReview
QuantityBelow.StoreOwnerNotification | Store, Product
RecurringPaymentCancelled.StoreOwnerNotification | Store, Order, Customer, RecurringPayment
ReturnRequestStatusChanged.CustomerNotification | Store, Customer, ReturnRequest, OrderItem
Service.EmailAFriend | Store, Customer, Product, PersonalMessage, CustomerEmail
ShipmentDelivered.CustomerNotification | Store, Shipment, Order, Customer
ShipmentSent.CustomerNotification | Store, Shipment, Order, Customer
VendorAccountApply.StoreOwnerNotification | Store, Customer, Vendor
VendorInformationChange.StoreOwnerNotification | Store, Vendor
Wishlist.EmailAFriend | Store, Customer, PersonalMessage, CustomerEmail


For the Domain Models of Customer, Store, Product, OrderItem etc. see the API:  https://nopcommerce.codeplex.com/SourceControl/latest#src/Libraries/Nop.Core/Domain/Customers/Customer.cs





How to Install
----
1. Download and extract to the *Plugins* Folder of your Web Application
2. Restart the nopCommerce Application
3. Install the Plugin on the Admin Site at */Admin/Plugin/List*
4. Done


Settings
----
* *razormessageservice.compiletask.enablelogging* - Enable/disable logging when the schedule task runs to compile messages. Default is *False*. This Setting was added in version 1.41.


Version History
----
###1.41
* Improved performance with caching of razor-mail-templates. First use of a mail-template might take up to 2 seconds to compile. Afterward no more recompilation is needed. Updating a mail template causes an automatic re-compilation. There's now a schedule task which automatically compiles all active mail-templates for all stores and languages to improve the performance when using a mail template the first time after an application restart.
* Updated to RazorEngine 3.9.3
* Works with nopCommerce 3.80

###1.40
* Works with nopCommerce 3.80

###1.30
* Works with nopCommerce 3.70

###1.20
* Works with nopCommerce 3.50

###1.11
* "OrderPaid.CustomerNotification" is now sent in the Language the Order was made
* Works with nopCommerce 3.40

###1.10
* Added new Message Template "OrderPaid.CustomerNotification"
* Works with nopCommerce 3.40 

###1.00
* Works with nopCommerce 3.40


Debugging Notes
----
* Errors in a template will result in an unparsed email, but with error-details at the end in the mail-body.
* Change setting "razormessageservice.compiletask.enablelogging" to "true" will log the result of a pre-compilation of all templates. Templates containing errors will be logged as "success: False"


[RazorEngine]:https://github.com/Antaris/RazorEngine
