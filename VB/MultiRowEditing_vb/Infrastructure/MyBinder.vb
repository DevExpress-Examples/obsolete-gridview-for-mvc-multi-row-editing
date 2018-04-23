Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports System.Web.Script.Serialization
Imports System.Collections.Specialized


Namespace MultiRowEditing.Infrastructure
    Public Class MyDictionaryModelBinder
        Implements IModelBinder
        Public Function BindModel(ByVal controllerContext As ControllerContext, ByVal bindingContext As ModelBindingContext) As Object Implements IModelBinder.BindModel
            Dim model As Dictionary(Of String, Object) = TryCast(bindingContext.Model, Dictionary(Of String, Object))
            If model Is Nothing Then
                model = TryCast(DependencyResolver.Current.GetService(GetType(Dictionary(Of String, Object))), Dictionary(Of String, Object))
            End If
            If bindingContext.ModelType Is GetType(Dictionary(Of String, Object)) Then
                DoFillModel(model, controllerContext.RequestContext.HttpContext.Request.Form, controllerContext, bindingContext)
            End If
            Return model
        End Function
        Private Sub DoFillModel(ByVal model As Dictionary(Of String, Object), ByVal nameValueCollection As NameValueCollection, ByVal controllerContext As ControllerContext, ByVal bindingContext As ModelBindingContext)
            For Each key As String In nameValueCollection.Keys
                If bindingContext.PropertyFilter(key) Then
                    Dim strValue As String = nameValueCollection(key)
                    Dim newModel As Dictionary(Of String, Object) = DoDeserializeString(strValue)
                    If String.IsNullOrEmpty(key) Then
                        For Each k As String In newModel.Keys
                            model.Add(k, newModel(k))
                        Next k
                    End If
                End If
            Next key
        End Sub
        Private Function DoDeserializeString(ByVal strValue As String) As Dictionary(Of String, Object)
            Dim serializer As JavaScriptSerializer = New JavaScriptSerializer()
            Return serializer.Deserialize(Of Dictionary(Of String, Object))(strValue)
        End Function
    End Class
End Namespace