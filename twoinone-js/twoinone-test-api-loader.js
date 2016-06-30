
var exports = {};

var extension = {
    
    setMessageListener: function(callback) {
        
        _setMessageListenerCalled = true;
    }
};

var _setMessageListenerCalled = false;

function twoinone_test_load_api(path) {

    var scriptElement = document.createElement('script');
    scriptElement.setAttribute("type","text/javascript");
    scriptElement.setAttribute("src", path);
    document.getElementsByTagName("head")[0].appendChild(scriptElement);
    
    return _setMessageListenerCalled ? exports : null;    
}
