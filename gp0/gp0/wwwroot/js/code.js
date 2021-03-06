﻿/// <reference path="code_async.js" />
/// <reference path="code_async.js" />
var isPluginEnabled = false;
var async_code_included = 1;
var global_isFromCont = new Array();
var global_selectbox_counter = 0;
var async_Promise;
var async_resolve;
var global_selectbox_container = new Array();
function MakeXMLSign(dataToSign, certObject) {
    try {
        var oSigner = cadesplugin.CreateObject("CAdESCOM.CPSigner");
    } catch (err) {
        errormes = "Failed to create CAdESCOM.CPSigner: " + err.number;
        alert(errormes);
        throw errormes;
    }

    if (oSigner) {
        oSigner.Certificate = certObject;
    }
    else {
        errormes = "Failed to create CAdESCOM.CPSigner";
        alert(errormes);
        throw errormes;
    }
    var signMethod = "";
    var digestMethod = "";
    var pubKey = certObject.PublicKey();
    var algo = pubKey.Algorithm;
    var algoOid = algo.Value;
    if (algoOid === "1.2.643.7.1.1.1.1") {   // алгоритм подписи ГОСТ Р 34.10-2012 с ключом 256 бит
        signMethod = "urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34102012-gostr34112012-256";
        digestMethod = "urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34112012-256";
    }
    else if (algoOid === "1.2.643.7.1.1.1.2") {   // алгоритм подписи ГОСТ Р 34.10-2012 с ключом 512 бит
        signMethod = "urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34102012-gostr34112012-512";
        digestMethod = "urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34112012-512";
    }
    else if (algoOid === "1.2.643.2.2.19") {  // алгоритм ГОСТ Р 34.10-2001
        signMethod = "urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34102001-gostr3411";
        digestMethod = "urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr3411";
    }
    else {
        errormes = "Данная страница поддерживает XML подпись сертификатами с алгоритмом ГОСТ Р 34.10-2012, ГОСТ Р 34.10-2001";
        throw errormes;
    }

    var CADESCOM_XML_SIGNATURE_TYPE_ENVELOPED = 0;

    try {
        var oSignedXML = cadesplugin.CreateObject("CAdESCOM.SignedXML");
    } catch (err) {
        alert('Failed to create CAdESCOM.SignedXML: ' + cadesplugin.getLastError(err));
        return;
    }
    oSignedXML.Content = dataToSign;
    oSignedXML.SignatureType = CADESCOM_XML_SIGNATURE_TYPE_ENVELOPED;
    oSignedXML.SignatureMethod = signMethod;
    oSignedXML.DigestMethod = digestMethod;
    var sSignedMessage = "";
    try {
        sSignedMessage = oSignedXML.Sign(oSigner);
    }
    catch (err) {
        errormes = "Не удалось создать подпись из-за ошибки: " + cadesplugin.getLastError(err);
        alert(errormes);
        throw errormes;
    }

    return sSignedMessage;
}

function Common_CheckForPlugIn() {
    var canAsync = !!cadesplugin.CreateObjectAsync;
    if (canAsync) {
        return CheckForPlugIn_Async();
    } else {
        return CheckForPlugIn();
    }
}

function include_async_code() {
    if (async_code_included) {
        return async_Promise;
    }
    var fileref = document.createElement('script');
    fileref.setAttribute("type", "text/javascript");
    fileref.setAttribute("src", "../js/code_async.js");
    document.getElementsByTagName("head")[0].appendChild(fileref);
    async_Promise = new Promise(function (resolve, reject) {
        async_resolve = resolve;
    });
    async_code_included = 1;
    return async_Promise;
}

function randomString(len) {
    var charSet = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    var newRandomString = '';
    for (var i = 0; i < len; i++) {
        var randomPoz = Math.floor(Math.random() * charSet.length);
        newRandomString += charSet.substring(randomPoz, randomPoz + 1);
    }
    return newRandomString;
}

function SignCadesXML(certListBoxId, dataToSign) {
    var certificate = GetCertificate(certListBoxId);
    var x = document.getElementById("Success1");
    try {
        var signature = MakeXMLSign(dataToSign, certificate);
        return signature;
    }
    catch (err) {
        if (x !== null) {
            x.innerText = "Возникла ошибка:";
        }
        x.innerText += " " + err;
    }
}
function MakeRequestCertificate(dataText, email, method, errorMessage) {
    var xhr = new XMLHttpRequest();
    var data = new FormData();
    x = document.getElementById(errorMessage);
    data.append("text", dataText);
    data.append("email", email);
    xhr.open('POST', method, true);
    var xhrTimeout;
    xhr.onreadystatechange = function () {
        if (xhr.readyState !== 4) return clearTimeout(xhrTimeout);
        if (xhr.status === 200) {
            var success = JSON.parse(xhr.responseText);
            if (success.correct === false) {
                x.innerHTML = "Ошибка " + success.text + "<b>";
            } else {
                document.location.href = "/home";
            }
        } else {
            x.innerHTML = xhr.statusText;
        }
    }
    xhr.send(data);
    xhrTimeout = setTimeout(function () {
        xhr.abort();
        x.innerHTML = "Timeout";
    },
        10000);
}
function RegCertificate(certListBoxId) {
    var x = document.getElementById("Success1");
    var id = randomString(256);
    var dataToSign;
    dataToSign = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" + "<test>\n" + id + "\n</test>";
    var signature = null;
    try {
        var canAsync = !!cadesplugin.CreateObjectAsync;
        if (canAsync) {
            RegCertificate_Async(certListBoxId,dataToSign);
        } else {
            signature = SignCadesXML(certListBoxId, dataToSign);
            CreateUserRequest(signature);
        }
    } catch (error) {
        x.innerText = error;
    }
}
function CreateUserRequest(signature) {
    var x = document.getElementById("Success1");
    var emailElement = document.getElementById("exampleFormControlInput1");
    var loginElement = document.getElementById("exampleFormControlInput2");
    var passwordElement = document.getElementById("exampleFormControlInput3");
    var full_nameElement = document.getElementById("exampleFormControlInput4");
    var positionElement = document.getElementById("exampleFormControlInput5");
    var email = emailElement.value;
    var login = loginElement.value;
    var password = passwordElement.value;
    var full_name = full_nameElement.value;
    var position = positionElement.value;
    var xhr = new XMLHttpRequest();
    var data = new FormData();
    data.append("email", email);
    data.append("login", login);
    data.append("password", password);
    data.append("full_name", full_name);
    data.append("position", position);
    data.append("sign", signature);
    xhr.open('POST', '/admin/createuser', true);
    var xhrTimeout;
    xhr.onreadystatechange = function() {
        if (xhr.readyState !== 4) return clearTimeout(xhrTimeout);
        if (xhr.status === 200) {
            var response = JSON.parse(xhr.responseText);
            emailElement.innerText = response.email;
            loginElement.innerText = response.login;
            full_nameElement.innerText = response.full_name;
            positionElement.innerText = response.position;
            x.innerText = response.error;
            //if (success.correct === false) {
            //    x.innerHTML = "Ошибка " + success.text + "<b>";
            //} else {
            //    document.location.href = "/home";
            //}
        } else {
            x.innerHTML = xhr.statusText;
        }
        return clearTimeout(xhrTimeout);
    };
    xhr.send(data);
    xhrTimeout = setTimeout(function () {
            xhr.abort();
            x.innerHTML = "Timeout";
        },
        10000);
}
function AuthCertificate(certListBoxId) {
    var canAsync = !!cadesplugin.CreateObjectAsync;
    if (canAsync) {
        AuthCertificate_Async(certListBoxId);
        return;
    }
    var x = document.getElementById("Success1");
    var id = randomString(256);
    var dataToSign;
    dataToSign = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" + "<test>\n" + id + "\n</test>";
    var signature = null;
    try {
        signature = SignCadesXML(certListBoxId, dataToSign);
        if (signature !== null)
            MakeRequestCertificate(signature, "", "/Auth/LoginCertificate", "Success1");
    } catch (error) {
        x.innerText = error;
    }
}

function HideArea() {
    var x = document.getElementById("textArea");
    if (x.hidden)
        x.hidden = false;
    else x.hidden = true;
}

function SendFile(certListBoxId) {
    if (!window.FileReader) {
        alert('Браузер не поддерживает загрузку файлов');
    }
    var input = document.getElementById("InputXml");
    var oFile = input.files[0];
    var oFReader = new FileReader();
    if (typeof (oFReader.readAsText) !== "function") {
        alert("Чтение файла не поддерживается");
        return;
    }
    oFReader.readAsText(oFile);
    oFReader.onload = function (oFREvent) {
        var sFileData = oFREvent.target.result;
        var canAsync = !!cadesplugin.CreateObjectAsync;
        if (confirm("Отправить документ?")) {
            if (canAsync) {
                SendXml_Async(certListBoxId, sFileData);
                input.value = null;
                return;
            }
            SendXml(certListBoxId, sFileData);
            input.value = null;
        } else input.value = null;
    };
}

function SendXml(certListBoxId, dataToSign) {
    var canAsync = !!cadesplugin.CreateObjectAsync;
    if (canAsync) {
        SendXml_Async(certListBoxId,null);
        return;
    }
    var x = document.getElementById("Success1");
    var email = document.getElementById('ReceiverEmail').value;
    if (email === null) {
        x.innerHTML = "Введите email получателя";
        return;
    }
    if (dataToSign === null)
        dataToSign = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" + document.getElementById("DataToSignTxtBox").value;
    var signature = null;
    try {
        signature = SignCadesXML(certListBoxId, dataToSign);
        if (signature !== null)
            MakeRequestDocument(signature, email, "Success1");
    } catch (err) {
        if (x !== null) {
            x.innerHTML = "Возникла ошибка:";
        }
        x.innerHTML += " " + err;
        return;
    }
}
function MakeRequestDocument(signature, email, errorMessage) {
    var xhr = new XMLHttpRequest();
    var data = new FormData();
    var x = document.getElementById(errorMessage);
    data.append("text", signature);
    data.append("receiver", email);
    xhr.open('POST', '/Home/SendDoc', true);
    var xhrTimeout;
    xhr.onreadystatechange = function () {
        if (xhr.readyState !== 4) return clearTimeout(xhrTimeout);
        if (xhr.status === 200) {
            var success = JSON.parse(xhr.responseText);
            if (success.correct === false) {
                x.innerHTML = "Ошибка " + success.text + "<b>";
                return;
            } else {
                x.innerHTML = "Документ успешно отправлен";
            }
        } else {
            x.innerHTML = xhr.statusText;
        }
    };
    xhr.send(data);
    xhrTimeout = setTimeout(function () {
        xhr.abort();
        x.innerHTML = "Timeout";
    },
        10000);
}
function FillCertInfo(certificate, certBoxId) {
    var ValidToDate = new Date(certificate.ValidToDate);
    var ValidFromDate = new Date(certificate.ValidFromDate);
    var IsValid = certificate.IsValid().Result;
    var hasPrivateKey = certificate.HasPrivateKey();
    var Now = new Date();

    var certObj = new CertificateObj(certificate);
    document.getElementById("cert_txt").setAttribute('style', 'visibility: visible;');
    document.getElementById(certBoxId).style.display = '';
    document.getElementById("subject").innerHTML = "Владелец: <b>" + certObj.GetCertName() + "<b>";
    document.getElementById("issuer").innerHTML = "Издатель: <b>" + certObj.GetIssuer() + "<b>";
    document.getElementById("from").innerHTML = "Выдан: <b>" + certObj.GetCertFromDate() + "<b>";
    document.getElementById("till").innerHTML = "Действителен до: <b>" + certObj.GetCertTillDate() + "<b>";
    if (hasPrivateKey) {
        document.getElementById("provname").innerHTML = "Криптопровайдер: <b>" + certObj.GetPrivateKeyProviderName() + "<b>";
    }
    document.getElementById("algorithm").innerHTML = "Алгоритм ключа: <b>" + certObj.GetPubKeyAlgorithm() + "<b>";
    if (Now < ValidFromDate) {
        document.getElementById("status").innerHTML = "Статус: <span style=\"color:red; font-weight:bold; font-size:16px\"><b>Срок действия не наступил</b></span>";
    } else if (Now > ValidToDate) {
        document.getElementById("status").innerHTML = "Статус: <span style=\"color:red; font-weight:bold; font-size:16px\"><b>Срок действия истек</b></span>";
    } else if (!hasPrivateKey) {
        document.getElementById("status").innerHTML = "Статус: <span style=\"color:red; font-weight:bold; font-size:16px\"><b>Нет привязки к закрытому ключу</b></span>";
    } else if (!IsValid) {
        document.getElementById("status").innerHTML = "Статус: <span style=\"color:red; font-weight:bold; font-size:16px\"><b>Ошибка при проверке цепочки сертификатов</b></span>";
    } else {
        document.getElementById("status").innerHTML = "Статус: <b> Действителен<b>";
    }
}
function getXmlHttp() {
    var xmlhttp;
    try {
        xmlhttp = new ActiveXObject("Msxml2.XMLHTTP");
    } catch (e) {
        try {
            xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
        } catch (E) {
            xmlhttp = false;
        }
    }
    if (!xmlhttp && typeof XMLHttpRequest !== 'undefined') {
        xmlhttp = new XMLHttpRequest();
    }
    return xmlhttp;
}
function CheckDoc(text) {
    var signedMessage = document.getElementById(text).value;
    var canAsync = !!cadesplugin.CreateObjectAsync;
    if (canAsync) {
        CheckDoc_Async(text);
        return;
    }
    if (Verify(signedMessage)) {
        document.getElementById('result').hidden = false;
        document.getElementById('Success1').innerText = "Подпись математически корректна";
    }
    else document.getElementById().innerText = "Подпись не действительна";
    document.getElementById('result').hidden=false;
    GetCertificateInfo(signedMessage);
}
function GetCertificateInfo(text) {
    var xhr = new XMLHttpRequest();
    var data = new FormData();
    x = document.getElementById('message');
    data.append("text", text);
    data.append("email", 'kek');
    xhr.open('POST',"/Home/CertificateInfo", true);
    var xhrTimeout;
    xhr.onreadystatechange = function () {
        if (xhr.readyState !== 4) return clearTimeout(xhrTimeout);
        if (xhr.status === 200) {
            var certificate = JSON.parse(xhr.responseText);
            document.getElementById("resultCertificate").hidden = false;
            document.getElementById("subject").innerHTML = "Владелец: <b>" + certificate.subject + "<b>";
            document.getElementById("issuer").innerHTML = "Издатель: <b>" + certificate.issuer + "<b>";
            document.getElementById("dateto").innerHTML = "Действителен до: <b>" + certificate.dateto + "<b>";
            document.getElementById("algorithm").innerHTML = "Алгоритм ключа: <b>" + certificate.algorithm + "<b>";
            if (certificate.valid)
                document.getElementById("valid").innerHTML = "Статус: <b> Действителен<b>";
            else
                document.getElementById("valid").innerHTML = "Статус: <b> Не действителен<b>";
            document.getElementById("message").innerHTML = "Сообщение от сервера: <b>" + certificate.message + "<b>";
        } else {
            x.innerHTML = xhr.statusText;
        }
    }
    xhr.send(data);
    xhrTimeout = setTimeout(function () {
            xhr.abort();
            x.innerHTML = "Timeout";
        },
        10000);
}
function Verify(signedMessage) {
    var oSignedXML = cadesplugin.CreateObject("CAdESCOM.SignedXML");
    try {
        oSignedXML.Verify(signedMessage);
        return true;
    } catch (err) {
        return false;
    }
}
function GetCertificate(certListBoxId) {
    var e = document.getElementById(certListBoxId);
    var selectedCertID = e.selectedIndex;
    if (selectedCertID === -1) {
        alert("Select certificate");
        return;
    }

    var thumbprint = e.options[selectedCertID].value.split(" ").reverse().join("").replace(/\s/g, "").toUpperCase();
    try {
        var oStore = cadesplugin.CreateObject("CAdESCOM.Store");
        oStore.Open();
    } catch (err) {
        alert('Certificate not found');
        return;
    }

    var CAPICOM_CERTIFICATE_FIND_SHA1_HASH = 0;
    var oCerts = oStore.Certificates.Find(CAPICOM_CERTIFICATE_FIND_SHA1_HASH, thumbprint);

    if (oCerts.Count === 0) {
        alert("Certificate not found");
        return;
    }
    var oCert = oCerts.Item(1);
    return oCert;
}
function FillCertList(lstId) {
    try {
        var oStore = cadesplugin.CreateObject("CAdESCOM.Store");
        oStore.Open();
    }
    catch (ex) {
        alert("Certificate not found");
        return;
    }
    try {
        var lst = document.getElementById(lstId);
        if (!lst)
            return;
    }
    catch (ex) {
        return;
    }
    lst.onchange = onCertificateSelected;
    lst.boxId = lstId;
    var certCnt;
    try {
        certCnt = oStore.Certificates.Count;
        if (certCnt === 0)
            throw "Certificate not found";
    }
    catch (ex) {
        oStore.Close();
        var errormes = document.getElementById("boxdiv").style.display = '';
        return;
    }
    for (var i = 1; i <= certCnt; i++) {
        var cert;
        try {
            cert = oStore.Certificates.Item(i);
        }
        catch (ex) {
            alert("Ошибка при перечислении сертификатов: " + cadesplugin.getLastError(ex));
            return;
        }
        var oOpt = document.createElement("OPTION");
        try {
            var certObj = new CertificateObj(cert);
            oOpt.text = certObj.GetCertString();
        }
        catch (ex) {
            alert("Ошибка при получении свойства SubjectName: " + cadesplugin.getLastError(ex));
        }
        try {
            oOpt.value = cert.Thumbprint;
        }
        catch (ex) {
            alert("Ошибка при получении свойства Thumbprint: " + cadesplugin.getLastError(ex));
        }
        lst.options.add(oOpt);
    }
    oStore.Close();
}
function CheckForPlugIn() {
    function VersionCompare(StringVersion, ObjectVersion) {
        if (typeof (ObjectVersion) === "string")
            return -1;
        var arr = StringVersion.split('.');

        if (ObjectVersion.MajorVersion === parseInt(arr[0])) {
            if (ObjectVersion.MinorVersion === parseInt(arr[1])) {
                if (ObjectVersion.BuildVersion === parseInt(arr[2])) {
                    return 0;
                }
                else if (ObjectVersion.BuildVersion < parseInt(arr[2])) {
                    return -1;
                }
            } else if (ObjectVersion.MinorVersion < parseInt(arr[1])) {
                return -1;
            }
        } else if (ObjectVersion.MajorVersion < parseInt(arr[0])) {
            return -1;
        }

        return 1;
    }
    function GetLatestVersion(CurrentPluginVersion) {
        var xmlhttp = getXmlHttp();
        xmlhttp.open("GET", "/sites/default/files/products/cades/latest_2_0.txt", true);
        xmlhttp.onreadystatechange = function () {
            var PluginBaseVersion;
            if (xmlhttp.readyState === 4) {
                if (xmlhttp.status === 200) {
                    PluginBaseVersion = xmlhttp.responseText;
                    if (isPluginWorked) { // плагин работает, объекты создаются
                        if (VersionCompare(PluginBaseVersion, CurrentPluginVersion) < 0) {
                            document.getElementById('PlugInEnabledTxt').innerHTML = "Плагин загружен, но есть более свежая версия.";
                            document.getElementById('info').setAttribute('class', 'alert alert-warning');
                        }
                    }
                    else { // плагин не работает, объекты не создаются
                        if (isPluginLoaded) { // плагин загружен
                            if (!isPluginEnabled) { // плагин загружен, но отключен
                                document.getElementById('PlugInEnabledTxt').innerHTML = "Плагин загружен, но отключен в настройках браузера.";
                                document.getElementById('info').setAttribute('class', 'alert alert-warning');
                            }
                            else { // плагин загружен и включен, но объекты не создаются
                                document.getElementById('PlugInEnabledTxt').innerHTML = "Плагин загружен, но не удается создать объекты. Проверьте настройки браузера.";
                                document.getElementById('info').setAttribute('class', 'alert alert-warning');
                            }
                        }
                        else { // плагин не загружен
                            document.getElementById('PlugInEnabledTxt').innerHTML = "Плагин не загружен.";
                            document.getElementById('info').setAttribute('class', 'alert alert-danger');
                        }
                    }
                }
            }
        }
        xmlhttp.send(null);
    }

    var isPluginLoaded = false;
    var isPluginWorked = false;
    var isActualVersion = false;
    try {
        var oAbout = cadesplugin.CreateObject("CAdESCOM.About");
        isPluginLoaded = true;
        isPluginEnabled = true;
        isPluginWorked = true;
        var CurrentPluginVersion = oAbout.PluginVersion;
        if (typeof (CurrentPluginVersion) === "undefined")
            CurrentPluginVersion = oAbout.Version;
        //document.getElementById('PlugInEnabledTxt').innerText = "Плагин загружен.";
        //document.getElementById('info').setAttribute('class', 'alert alert-success');
    }
    catch (err) {
        var mimetype = navigator.mimeTypes["application/x-cades"];
        if (mimetype) {
            isPluginLoaded = true;
            var plugin = mimetype.enabledPlugin;
            if (plugin) {
                isPluginEnabled = true;
            }
        }
    }
    GetLatestVersion(CurrentPluginVersion);
    if (location.pathname.indexOf("symalgo_sample.html") >= 0) {
        FillCertList('CertListBox1');
        FillCertList('CertListBox2');
    } else {
        FillCertList('CertListBox');
    }
}
function onCertificateSelected(event) {
    var selectedCertID = event.target.selectedIndex;
    var thumbprint = event.target.options[selectedCertID].value.split(" ").reverse().join("").replace(/\s/g, "").toUpperCase();
    try {
        var oStore = cadesplugin.CreateObject("CAdESCOM.Store");
        oStore.Open();
    } catch (err) {
        alert('Certificate not found');
        return;
    }

    var all_certs = oStore.Certificates;
    var oCerts = all_certs.Find(cadesplugin.CAPICOM_CERTIFICATE_FIND_SHA1_HASH, thumbprint);

    if (oCerts.Count === 0) {
        alert("Certificate not found");
        return;
    }
    var certificate = oCerts.Item(1);
    FillCertInfo(certificate, event.target.boxId);
}
function Find_Cert() {
    var xhr = new XMLHttpRequest();
    var data = new FormData();
    var Data = document.getElementById('DataToVerifyTxtBox').value;
    data.append("text", Data);
    var xhrTimeout=100;
    xhr.open('POST', '/home/Verify', true);
    xhr.onreadystatechange = function () {
        if (xhr.readyState !== 4) return;
        clearTimeout(xhrTimeout);
        if (xhr.status === 200) {
            var cert = JSON.parse(xhr.responseText);
            if (cert.SerialNumber === null) {
                document.getElementById("cert_txt_verify").setAttribute('style', 'visibility: hidden;');
                document.getElementById("message_verify").innerHTML = "Сообщение от сервера: <b>" + cert.Message + "<b>";
                document.getElementById("cert_verify_info").hidden = true;
            }
            else {
                document.getElementById("cert_txt_verify").setAttribute('style', 'visibility: visible;');
                document.getElementById("cert_verify_info").style.display = '';
                document.getElementById("cert_verify_info").hidden = false;
                document.getElementById("subject_verify").innerHTML = "Владелец: <b>" + cert.SubjectName + "<b>";
                document.getElementById("issuer_verify").innerHTML = "Издатель: <b>" + cert.IssuerName + "<b>";
                document.getElementById("algorithm_verify").innerHTML = "Алгоритм: <b>" + cert.Algorithm + "<b>";
                document.getElementById("notafter_verify").innerHTML = "Действителен до: <b>" + cert.NotAfter + "<b>";
                document.getElementById("serial_number_verify").innerHTML = "Серийный номер: <b>" + cert.SerialNumber + "<b>";
                document.getElementById("message_verify").innerHTML = "Сообщение от сервера: <b>" + cert.Message + "<b>";
            }
        } else {
            handleError(xhr.statusText)
        }
    }
    xhr.send(data);
    xhrTimeout = setTimeout(function () { xhr.abort(); handleError("Timeout") }, 10000);

    function handleError(message) {
        document.getElementById("cert_txt_verify").setAttribute('style', 'visibility: hidden;');
        document.getElementById("message_verify").innerHTML = "Ошибка при попытке связи с сервером";
    }
}
function CertificateObj(certObj) {
    this.cert = certObj;
    this.certFromDate = new Date(this.cert.ValidFromDate);
    this.certTillDate = new Date(this.cert.ValidToDate);
}

CertificateObj.prototype.check = function (digit) {
    return (digit < 10) ? "0" + digit : digit;
}
CertificateObj.prototype.extract = function (from, what) {
    certName = "";

    var begin = from.indexOf(what);

    if (begin >= 0) {
        var end = from.indexOf(', ', begin);
        certName = (end < 0) ? from.substr(begin) : from.substr(begin, end - begin);
    }

    return certName;
}

CertificateObj.prototype.DateTimePutTogether = function (certDate) {
    return this.check(certDate.getUTCDate()) + "." + this.check(certDate.getMonth() + 1) + "." + certDate.getFullYear() + " " +
        this.check(certDate.getUTCHours()) + ":" + this.check(certDate.getUTCMinutes()) + ":" + this.check(certDate.getUTCSeconds());
}

CertificateObj.prototype.GetCertString = function () {
    return this.extract(this.cert.SubjectName, 'CN=') + "; Выдан: " + this.GetCertFromDate();
}

CertificateObj.prototype.GetCertFromDate = function () {
    return this.DateTimePutTogether(this.certFromDate);
}

CertificateObj.prototype.GetCertTillDate = function () {
    return this.DateTimePutTogether(this.certTillDate);
}

CertificateObj.prototype.GetPubKeyAlgorithm = function () {
    return this.cert.PublicKey().Algorithm.FriendlyName;
}

CertificateObj.prototype.GetCertName = function () {
    return this.extract(this.cert.SubjectName, 'CN=');
}

CertificateObj.prototype.GetIssuer = function () {
    return this.extract(this.cert.IssuerName, 'CN=');
}

CertificateObj.prototype.GetPrivateKeyProviderName = function () {
    return this.cert.PrivateKey.ProviderName;
}

