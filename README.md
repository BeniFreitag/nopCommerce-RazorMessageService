nopCommerce Razor MessageService
=========

This Plugin extends the core nopCommerce Message Template Engine with Razor-Support (by [RazorEngine]).

Extend any nopCommerce Message Template with the full power of Razor. It works as a Post-Processor of the Token-Engine, so nothing must be changed. Token will continue to work.

Example of "OrderPlaced.CustomerNotification"

```
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
Adapt "/Administration/Views/Shared/EditorTemplates/RichEditor.cshtml"
```
tinyMCE.init({
    ...
	extended_valid_elements: "text",
	forced_root_blocks: false
});
```


Supported Message Templates
----
* Blog.BlogComment
* Customer.BackInStock
* Customer.EmailValidationMessage
* Customer.NewOrderNote
* Customer.NewPM
* Customer.PasswordRecovery
* Customer.WelcomeMessage
* Forums.NewForumPost
* Forums.NewForumTopic
* GiftCard.Notification
* NewCustomer.Notification
* NewReturnRequest.StoreOwnerNotification
* News.NewsComment
* NewsLetterSubscription.ActivationMessage
* NewVATSubmitted.StoreOwnerNotification
* OrderCancelled.CustomerNotification
* OrderCompleted.CustomerNotification
* OrderPaid.StoreOwnerNotification
* OrderPlaced.CustomerNotification
* OrderPlaced.StoreOwnerNotification
* OrderPlaced.VendorNotification
* Product.ProductReview
* QuantityBelow.StoreOwnerNotification
* RecurringPaymentCancelled.StoreOwnerNotification
* ReturnRequestStatusChanged.CustomerNotification
* Service.EmailAFriend
* ShipmentDelivered.CustomerNotification
* ShipmentSent.CustomerNotification
* Wishlist.EmailAFriend



Version
----
###1.00
#### Works with nopCommerce 3.40

[RazorEngine]:https://github.com/Antaris/RazorEngine
