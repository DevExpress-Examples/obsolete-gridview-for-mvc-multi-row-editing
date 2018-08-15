Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports MultiRowEditing.Models
Imports System.IO
Imports System.Web.Script.Serialization
Imports MultiRowEditing.Infrastructure
Imports System.Web.Mvc

Namespace MultiRowEditing.Controllers
    Public Class HomeController
        Inherits Controller

        Public Function Index() As ActionResult
            Return View(ProductRepository.GetProducts())
        End Function
        Public Function GridPartial() As ActionResult
            Return PartialView("GridViewPartial", ProductRepository.GetProducts())
        End Function
        <HttpPost>
        Public Function SaveData(<ModelBinder(GetType(MyDictionaryModelBinder))> ByVal changedValues As Dictionary(Of String, Object)) As JsonResult
            'bool res = ProductRepository.UpdateValues(changedValues); uncomment this line to save data
            Dim res As Boolean = True 'remove this line to test the project
            Dim result = New With {Key .ResultStatus = (If(res, "ok", "error"))}
            Return Json(result)
        End Function
    End Class
End Namespace
