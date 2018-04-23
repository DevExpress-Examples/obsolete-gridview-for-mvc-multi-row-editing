using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Collections.Specialized;


namespace MultiRowEditing.Infrastructure {
    public class MyDictionaryModelBinder : IModelBinder {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
            Dictionary<string, object> model = bindingContext.Model as Dictionary<string, object> ?? 
                                               DependencyResolver.Current.GetService(typeof(Dictionary<string, object>)) as Dictionary<string, object>;
            if (bindingContext.ModelType == typeof(Dictionary<string, object>)) 
                DoFillModel(model, controllerContext.RequestContext.HttpContext.Request.Form, controllerContext, bindingContext);               
            return model;
        }
        private void DoFillModel(Dictionary<string, object> model, NameValueCollection nameValueCollection, ControllerContext controllerContext, ModelBindingContext bindingContext) {
            foreach (string key in nameValueCollection.Keys) {
                if (bindingContext.PropertyFilter(key)) {
                    string strValue = nameValueCollection[key];
                    Dictionary<string, object> newModel = DoDeserializeString(strValue);
                    if (string.IsNullOrEmpty(key)) {
                        foreach (string k in newModel.Keys)
                            model.Add(k, newModel[k]);
                    }
                }
            }
        }
        private Dictionary<string, object> DoDeserializeString(string strValue) {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<Dictionary<string, object>>(strValue);
        }
    }
}