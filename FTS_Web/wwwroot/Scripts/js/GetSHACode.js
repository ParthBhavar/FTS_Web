

/*history.go(1);*/
/*-------------- function for User Portal registration. ---------------------*/
function UserPortalRegistration(seed, passwordid, hiddenfieldid) {
    
    var password = document.getElementById(passwordid).value;
    var errorMsg = '';
    var reg = new RegExp("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");

    document.getElementById(passwordid).value = "123456";
    document.getElementById(passwordid).visibility = "hidden";
    var h = Crypto.SHA1(password);
    document.getElementById(hiddenfieldid).value = h;
}
/*--------------------------------------------------------------------------*/
/*-------------- function for User Portal registration. ---------------------*/
function UserPortalLogin(seed, passwordid, hiddenfieldid) {
   
    var password = document.getElementById(passwordid).value;
    var errorMsg = '';
    var reg = new RegExp("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");
    //var reg = new RegExp("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*")

    document.getElementById(passwordid).value = "12312312312";
    document.getElementById(passwordid).visibility = "hidden";
    var h = Crypto.SHA1(password);
    var str = seed.toString() + h.toString().toUpperCase();
    var hash = Crypto.SHA1(str);
    document.getElementById(hiddenfieldid).value = hash;
    /*document.getElementById(passwordid).value = hash;*/
}
function UserPortalConfirmLogin(seed, passwordid, hiddenfieldid) {
    
    var password = document.getElementById(passwordid).value;
    var errorMsg = '';
    var reg = new RegExp("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");

    document.getElementById(passwordid).value = "12312312312";
    document.getElementById(passwordid).visibility = "hidden";
    var h = Crypto.SHA1(password);
    var str = seed.toString() + h.toString();
    var hash = Crypto.SHA1(str);
    document.getElementById(hiddenfieldid).value = hash;
    document.getElementById(passwordid).value = hash;
}
function UserPortalNewLogin(seed, passwordid, hiddenfieldid) {
    
    var password = document.getElementById(passwordid).value;
    var errorMsg = '';
    var reg = new RegExp("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");

    document.getElementById(passwordid).value = "12312312312";
    document.getElementById(passwordid).visibility = "hidden";
    var h = Crypto.SHA1(password);
    var str = seed.toString() + h.toString();
    var hash = Crypto.SHA1(str);
    document.getElementById(hiddenfieldid).value = hash;
    document.getElementById(passwordid).value = hash;
}

/*--------------------------------------------------------------------------*/
function UserPortaloldPWD(seed, passwordid, hiddenfieldid) {
    
    var password = document.getElementById(passwordid).value;
    var errorMsg = '';
    var reg = new RegExp("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");

    document.getElementById(passwordid).value = "12312312312";
    document.getElementById(passwordid).visibility = "hidden";
    var h = Crypto.SHA1(password);
    var str = seed.toString() + h.toString();
    var hash = Crypto.SHA1(str);
    document.getElementById(hiddenfieldid).value = hash;
    document.getElementById(passwordid).value = hash;
}
//----------------------------------------------------------------------------------------------


function PWD(password) {
    var h = Crypto.SHA1(password);
    return h;
}
