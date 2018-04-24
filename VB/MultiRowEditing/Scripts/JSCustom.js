var changedValues = {};
function OnValueTextChanged(s, e, keyValue, fieldName) {
    var currentRow = null;
    if (changedValues.hasOwnProperty(keyValue)) 
        currentRow = changedValues[keyValue];
    else {
        currentRow = {};
        changedValues[keyValue] = currentRow;
    }
    currentRow[fieldName] = s.GetValue();
}
function OnButtonClick(s, e, url_par) {
    s.SetEnabled(false);
    $.ajax({
        type: 'POST',
        url: url_par,
        data: JSON.stringify(changedValues),
        dataType: 'json'
    }).done(CallbackComplete).fail(OnCallbackError);
}
function CallbackComplete(data) {
    changedValues = {};
    alert(data.ResultStatus == 'ok' ? 'Updated successfully' : 'Error occured during updating')
    btnSave.SetEnabled(true);
}
function OnCallbackError() {
    alert('Callback error');
    btnSave.SetEnabled(true);
}