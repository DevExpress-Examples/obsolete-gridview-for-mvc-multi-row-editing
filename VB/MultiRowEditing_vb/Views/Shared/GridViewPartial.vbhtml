@ModelType System.Data.DataTable
@Html.DevExpress().GridView( _
    Sub(settings)
            settings.Name = "grid"
            settings.CallbackRouteValues = New With {Key .Controller = "Home", Key .Action = "GridPartial"}
            settings.KeyFieldName = "ProductID"
            settings.Columns.Add("ProductID")
            settings.Columns.Add( _
                Sub(col)
                        col.FieldName = "ProductName"
                        col.SetDataItemTemplateContent( _
                             Sub(diTemplate)
                                     Dim container As GridViewDataItemTemplateContainer = TryCast(diTemplate, GridViewDataItemTemplateContainer)
                                     Html.DevExpress().TextBox( _
                                         Sub(txtSettings)
                                                 txtSettings.Name = String.Format("txt_{0}", diTemplate.VisibleIndex)
                                                 txtSettings.Text = DataBinder.Eval(diTemplate.DataItem, diTemplate.Column.FieldName).ToString()
                                                 txtSettings.Properties.ClientSideEvents.TextChanged = String.Format("function(s, e) {{OnValueTextChanged(s, e, '{0}', '{1}'); }}", container.KeyValue.ToString(), container.Column.FieldName)
                                         End Sub).Render()
                             End Sub)
                End Sub)
            settings.Columns.Add( _
                Sub(col)
                        col.FieldName = "CategoryID"
                        col.SetDataItemTemplateContent( _
                            Sub(diTemplate)
                                    Dim container As GridViewDataItemTemplateContainer = TryCast(diTemplate, GridViewDataItemTemplateContainer)
                                    Html.DevExpress().ComboBox( _
                                        Sub(cmbSettings)
                                                cmbSettings.Name = String.Format("cmb_{0}", diTemplate.VisibleIndex)
                                                cmbSettings.Properties.ValueType = GetType(Integer)
                                                cmbSettings.Properties.ValueField = "CategoryID"
                                                cmbSettings.Properties.TextField = "CategoryName"
                                                cmbSettings.Properties.ClientSideEvents.ValueChanged = String.Format("function(s, e) {{OnValueTextChanged(s, e, '{0}', '{1}'); }}", container.KeyValue.ToString(), container.Column.FieldName)
                                        End Sub).BindList(MultiRowEditing_vb.MultiRowEditing.Models.CategoryRepository.GetCategories()).Bind(DataBinder.Eval(diTemplate.DataItem, diTemplate.Column.FieldName)).Render()
                            End Sub)
                End Sub)
    End Sub).Bind(Model).GetHtml()
