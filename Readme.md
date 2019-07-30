# How to implement the multi-row editing feature in the GridView
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/e4236/)**
<!-- run online end -->


<p><strong>UPDATED:</strong><br><br>Starting with version 13.2, the GridView control offers the basic "Batch Editing Mode" functionality that allows accomplishing a similar task with less effort and does not require so much extra code. See the <a href="https://community.devexpress.com/blogs/aspnet/archive/2013/12/16/asp-net-webforms-amp-mvc-gridview-batch-edit-what-39-s-new-in-13-2.aspx">ASP.NET WebForms & MVC: GridView Batch Edit </a> blog post to learn more about this new functionality.<br>Starting with version 14.1, the GridView control offers advanced "Batch Editing Mode" programming options.<br><br>You can find a standalone DB-independent solution in our Code Examples base at:<br><a href="https://www.devexpress.com/Support/Center/p/E5046">GridView - A simple Batch Editing implementation</a> <br><br>If you have version v14.1+ available, consider using the built-in functionality instead of the approach detailed below.<br>If you need further assistance with this functionality, please create a new ticket in our Support Center.<br><br>This example demonstrates how to implement the multi-row editing feature using the GridView extension. The main idea is to save changed values in the JavaScript dictionary and pass this dictionary to the required action. It is necessary to use a custom ModelBinder in the action to convert passed data to the required dictionary object.</p>
<p><strong>See also:</strong><strong><br> </strong><a href="https://www.devexpress.com/Support/Center/p/E3326">How to perform GridView instant updating using different editors in the DataItem template </a><br> <a href="https://www.devexpress.com/Support/Center/p/E4073">GridView - How to implement batch update using the Ajax request</a><strong><br> </strong><a href="https://www.devexpress.com/Support/Center/p/E324">How to implement the multi-row editing feature in the ASPxGridView</a><br> <a href="http://msdn.microsoft.com/en-us/library/system.web.mvc.imodelbinder(v=vs.98).aspx"><u>IModelBinder Interface</u></a><br> <a href="http://stackoverflow.com/questions/2343913/asp-net-mvc2-custom-model-binder-examples"><u>ASP.NET MVC2 - Custom Model Binder Examples</u></a><br> <a href="http://api.jquery.com/jQuery.ajax/"><u>jQuery.ajax()</u></a></p>
<p><br><strong>Web Forms:</strong><br><a href="https://www.devexpress.com/Support/Center/p/E324">How to implement the multi-row editing feature in the ASPxGridView</a></p>

<br/>


