Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports MultiRowEditing_vb.MultiRowEditing.Models
Imports System.IO
Imports System.Web.Script.Serialization
Imports MultiRowEditing_vb.MultiRowEditing.Infrastructure


Namespace MultiRowEditing.Controllers
	Public Class HomeController
		Inherits Controller
		Public Function Index() As ActionResult
			Return View(ProductRepository.GetProducts())
		End Function
		Public Function GridPartial() As ActionResult
			Return PartialView("GridViewPartial", ProductRepository.GetProducts())
		End Function
        <HttpPost()> _
        Public Function SaveData(<ModelBinder(GetType(MyDictionaryModelBinder))> ByVal changedValues As Dictionary(Of String, Object)) As JsonResult
            'Dim result = New With {Key .ResultStatus = ProductRepository.UpdateValues(changedValues)} uncomment this line to save data
            Dim result = New With {Key .ResultStatus = ProductRepository.UpdateValues(changedValues)} 'remove this line to test the project
            Return Json(result)
        End Function
	End Class
End Namespace
