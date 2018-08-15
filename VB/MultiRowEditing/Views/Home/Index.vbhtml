@ModelType System.Data.DataTable

@Code
    ViewData("Title") = "Index"
End Code

@Html.Partial("GridViewPartial", Model)
@Html.DevExpress().Button(
                Sub(settings)
                    settings.Name = "btnSave"
                    settings.Text = "Save changes"
                    settings.UseSubmitBehavior = False
                    settings.ClientSideEvents.Click = String.Format("function(s, e) {{ OnButtonClick(s, e, '{0}') }}", Url.Action("SaveData", "Home", Nothing))
                End Sub
            ).GetHtml()
