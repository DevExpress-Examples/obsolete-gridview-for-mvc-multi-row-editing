Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Data
Imports System.Data.OleDb

Namespace MultiRowEditing.Models
    Public Class ConnectionRepository
        Public Shared Function GetDataConnection() As OleDbConnection
            Dim connection As OleDbConnection = New OleDbConnection()
            connection.ConnectionString = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}", HttpContext.Current.Server.MapPath("~/App_Data/data.mdb"))
            Return connection
        End Function
    End Class
    Public Class ProductRepository
        Private Const updateCommandTemplate As String = "UPDATE [Products] SET {0} WHERE [ProductID] = {1}"
        Private Const fieldValueString As String = "[{0}] = '{1}'"
        Private Const fieldValueInt As String = "[{0}] = {1}"
        Private Sub New()
        End Sub
        Public Shared Function GetProducts() As DataTable
            Dim dataTableProducts As DataTable = New DataTable()
            Using connection As OleDbConnection = ConnectionRepository.GetDataConnection()
                Dim adapter As OleDbDataAdapter = New OleDbDataAdapter(String.Empty, connection)
                adapter.SelectCommand.CommandText = "SELECT [ProductID], [ProductName], [CategoryID] FROM [Products]"
                adapter.Fill(dataTableProducts)
            End Using
            Return dataTableProducts
        End Function
        Public Shared Function UpdateValues(ByVal changedValues As Dictionary(Of String, Object)) As String
            Dim updateResult As String = "ok"
            Try
                For Each row As String In changedValues.Keys
                    Dim fieldValues As String = String.Empty
                    Dim fields As Dictionary(Of String, Object) = TryCast(changedValues(row), Dictionary(Of String, Object))
                    Dim firstItemPresent As Boolean = False
                    For Each field As String In fields.Keys
                        If firstItemPresent Then
                            fieldValues &= ((", ") + String.Format(GetTemplateByFieldName(field), field, fields(field)))
                        Else
                            fieldValues &= ((String.Empty) + String.Format(GetTemplateByFieldName(field), field, fields(field)))
                        End If
                        If (Not firstItemPresent) Then
                            firstItemPresent = True
                        End If
                    Next field
                    Dim commandText As String = String.Format(updateCommandTemplate, fieldValues, row)
                    UpdateValue(commandText)
                Next row
            Catch
                updateResult = "error"
            End Try
            Return updateResult
        End Function
        Private Shared Sub UpdateValue(ByVal commandText As String)
            Using connection As OleDbConnection = ConnectionRepository.GetDataConnection()
                connection.Open()
                Dim cmd As OleDbCommand = New OleDbCommand(commandText, connection)
                cmd.ExecuteNonQuery()
            End Using
        End Sub
        Private Shared Function GetTemplateByFieldName(ByVal fieldName As String) As String
            Dim res As String = String.Empty
            Select Case fieldName
                Case "ProductName"
                    res = fieldValueString
                    Exit Select
                Case "CategoryID"
                    res = fieldValueInt
                    Exit Select
            End Select
            Return res
        End Function
    End Class
    Public Class CategoryRepository
        Public Shared Function GetCategories() As DataTable
            Dim dataTableCategories As DataTable = New DataTable()
            Using connection As OleDbConnection = ConnectionRepository.GetDataConnection()
                Dim adapter As OleDbDataAdapter = New OleDbDataAdapter(String.Empty, connection)
                adapter.SelectCommand.CommandText = "SELECT [CategoryID], [CategoryName] FROM [Categories]"
                adapter.Fill(dataTableCategories)
            End Using
            Return dataTableCategories
        End Function
    End Class
End Namespace