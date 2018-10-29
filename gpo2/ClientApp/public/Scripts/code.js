var isPluginEnabled = false;
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
    if (algoOid == "1.2.643.7.1.1.1.1") {   // �������� ������� ���� � 34.10-2012 � ������ 256 ���
        signMethod = "urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34102012-gostr34112012-256";
        digestMethod = "urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34112012-256";
    }
    else if (algoOid == "1.2.643.7.1.1.1.2") {   // �������� ������� ���� � 34.10-2012 � ������ 512 ���
        signMethod = "urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34102012-gostr34112012-512";
        digestMethod = "urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34112012-512";
    }
    else if (algoOid == "1.2.643.2.2.19") {  // �������� ���� � 34.10-2001
        signMethod = "urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34102001-gostr3411";
        digestMethod = "urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr3411";
    }
    else {
        errormes = "������ �������� ������������ XML ������� ������������� � ���������� ���� � 34.10-2012, ���� � 34.10-2001";
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
        errormes = "�� ������� ������� ������� ��-�� ������: " + cadesplugin.getLastError(err);
        alert(errormes);
        throw errormes;
    }

    return sSignedMessage;
}
function GetSignatureTitleElement() {
    var elementSignatureTitle = null;
    var x = document.getElementsByName("SignatureTitle");

    if (x.length == 0) {

        if (elementSignatureTitle.nodeName == "P") {
            return elementSignatureTitle;
        }
    }
    else {
        elementSignatureTitle = x[0];
    }

    return elementSignatureTitle;
}
function LookSign() {
    if (document.getElementById("SignatureTxtBox").hidden)
        document.getElementById("SignatureTxtBox").hidden = false;
    else document.getElementById("SignatureTxtBox").hidden = true;
}
function SignCadesXML(certListBoxId) {
    var certificate = GetCertificate(certListBoxId);
    document.getElementById("SignatureTxtBox").innerHTML = "";
    var dataToSign = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
        document.getElementById("DataToSignTxtBox").value;
    var x = GetSignatureTitleElement();
    try {
        var signature = MakeXMLSign(dataToSign, certificate);
        document.getElementById("SignatureTxtBox").innerHTML = signature;

        if (x != null) {
            x.innerHTML = "������� ������������ �������";
            document.getElementById("LookBtn").hidden = false;
            document.getElementById("DownloadBtn").hidden = false;
        }
    }
    catch (err) {
        if (x != null) {
            x.innerHTML = "�������� ������:";
        }
        document.getElementById("SignatureTxtBox").innerHTML = err;
        document.getElementById("LookBtn").hidden = true;
        document.getElementById("DownloadBtn").hidden = true;
    }
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
    document.getElementById("subject").innerHTML = "��������: <b>" + certObj.GetCertName() + "<b>";
    document.getElementById("issuer").innerHTML = "��������: <b>" + certObj.GetIssuer() + "<b>";
    document.getElementById("from").innerHTML = "�����: <b>" + certObj.GetCertFromDate() + "<b>";
    document.getElementById("till").innerHTML = "������������ ��: <b>" + certObj.GetCertTillDate() + "<b>";
    if (hasPrivateKey) {
        document.getElementById("provname").innerHTML = "���������������: <b>" + certObj.GetPrivateKeyProviderName() + "<b>";
    }
    document.getElementById("algorithm").innerHTML = "�������� �����: <b>" + certObj.GetPubKeyAlgorithm() + "<b>";
    if (Now < ValidFromDate) {
        document.getElementById("status").innerHTML = "������: <span style=\"color:red; font-weight:bold; font-size:16px\"><b>���� �������� �� ��������</b></span>";
    } else if (Now > ValidToDate) {
        document.getElementById("status").innerHTML = "������: <span style=\"color:red; font-weight:bold; font-size:16px\"><b>���� �������� �����</b></span>";
    } else if (!hasPrivateKey) {
        document.getElementById("status").innerHTML = "������: <span style=\"color:red; font-weight:bold; font-size:16px\"><b>��� �������� � ��������� �����</b></span>";
    } else if (!IsValid) {
        document.getElementById("status").innerHTML = "������: <span style=\"color:red; font-weight:bold; font-size:16px\"><b>������ ��� �������� ������� ������������</b></span>";
    } else {
        document.getElementById("status").innerHTML = "������: <b> ������������<b>";
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
    if (!xmlhttp && typeof XMLHttpRequest != 'undefined') {
        xmlhttp = new XMLHttpRequest();
    }
    return xmlhttp;
}

function Verify() {
    sSignedMessage = document.getElementById("DataToVerifyTxtBox").value;
    var oSignedXML = cadesplugin.CreateObject("CAdESCOM.SignedXML");
    try {
        oSignedXML.Verify(sSignedMessage);
        document.getElementById("txt_verify").setAttribute('style', 'visibility: visible;');
        document.getElementById("verify_info").style.display = '';
        document.getElementById("check_verify").innerHTML = "������� ������������" + "<b>";
    } catch (err) {
        document.getElementById("txt_verify").setAttribute('style', 'visibility: visible;');
        document.getElementById("verify_info").style.display = '';
        document.getElementById("check_verify").innerHTML = "������ � �������� ������� : " + cadesplugin.getLastError(err) + "<b>";
    }
}
function GetCertificate(certListBoxId) {
    var e = document.getElementById(certListBoxId);
    var selectedCertID = e.selectedIndex;
    if (selectedCertID == -1) {
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

    if (oCerts.Count == 0) {
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
        if (certCnt == 0)
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
            alert("������ ��� ������������ ������������: " + cadesplugin.getLastError(ex));
            return;
        }
        var oOpt = document.createElement("OPTION");
        try {
            var certObj = new CertificateObj(cert);
            oOpt.text = certObj.GetCertString();
        }
        catch (ex) {
            alert("������ ��� ��������� �������� SubjectName: " + cadesplugin.getLastError(ex));
        }
        try {
            oOpt.value = cert.Thumbprint;
        }
        catch (ex) {
            alert("������ ��� ��������� �������� Thumbprint: " + cadesplugin.getLastError(ex));
        }
        lst.options.add(oOpt);
    }
    oStore.Close();
}
function CheckForPlugIn() {
    function VersionCompare(StringVersion, ObjectVersion) {
        if (typeof (ObjectVersion) == "string")
            return -1;
        var arr = StringVersion.split('.');

        if (ObjectVersion.MajorVersion == parseInt(arr[0])) {
            if (ObjectVersion.MinorVersion == parseInt(arr[1])) {
                if (ObjectVersion.BuildVersion == parseInt(arr[2])) {
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

    function GetCSPVersion() {
        try {
            var oAbout = cadesplugin.CreateObject("CAdESCOM.About");
        } catch (err) {
            alert('Failed to create CAdESCOM.About: ' + cadesplugin.getLastError(err));
            return;
        }
        var ver = oAbout.CSPVersion("", 75);
        return ver.MajorVersion + "." + ver.MinorVersion + "." + ver.BuildVersion;
    }

    function GetCSPName() {
        var sCSPName = "";
        try {
            var oAbout = cadesplugin.CreateObject("CAdESCOM.About");
            sCSPName = oAbout.CSPName(75);

        } catch (err) {
        }
        return sCSPName;
    }

    function ShowCSPVersion(CurrentPluginVersion) {
        if (typeof (CurrentPluginVersion) != "string") {
            document.getElementById('CSPVersionTxt').innerHTML = "������ ����������������: " + GetCSPVersion();
        }
        var sCSPName = GetCSPName();
        if (sCSPName != "") {
            document.getElementById('CSPNameTxt').innerHTML = "���������������: " + sCSPName;
        }
    }
    function GetLatestVersion(CurrentPluginVersion) {
        var xmlhttp = getXmlHttp();
        xmlhttp.open("GET", "/sites/default/files/products/cades/latest_2_0.txt", true);
        xmlhttp.onreadystatechange = function () {
            var PluginBaseVersion;
            if (xmlhttp.readyState == 4) {
                if (xmlhttp.status == 200) {
                    PluginBaseVersion = xmlhttp.responseText;
                    if (isPluginWorked) { // ������ ��������, ������� ���������
                        if (VersionCompare(PluginBaseVersion, CurrentPluginVersion) < 0) {
                            document.getElementById('PlugInEnabledTxt').innerHTML = "������ ��������, �� ���� ����� ������ ������.";
                            document.getElementById('info').setAttribute('class', 'alert alert-warning');
                        }
                    }
                    else { // ������ �� ��������, ������� �� ���������
                        if (isPluginLoaded) { // ������ ��������
                            if (!isPluginEnabled) { // ������ ��������, �� ��������
                                document.getElementById('PlugInEnabledTxt').innerHTML = "������ ��������, �� �������� � ���������� ��������.";
                                document.getElementById('info').setAttribute('class', 'alert alert-warning');
                            }
                            else { // ������ �������� � �������, �� ������� �� ���������
                                document.getElementById('PlugInEnabledTxt').innerHTML = "������ ��������, �� �� ������� ������� �������. ��������� ��������� ��������.";
                                document.getElementById('info').setAttribute('class', 'alert alert-warning');
                            }
                        }
                        else { // ������ �� ��������
                            document.getElementById('PlugInEnabledTxt').innerHTML = "������ �� ��������.";
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
        if (typeof (CurrentPluginVersion) == "undefined")
            CurrentPluginVersion = oAbout.Version;
        document.getElementById('PlugInEnabledTxt').innerText = "������ ��������.";
        document.getElementById('info').setAttribute('class', 'alert alert-success');
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

    if (oCerts.Count == 0) {
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
    xhr.open('POST', '/home/Verify', true);
    xhr.onreadystatechange = function () {
        if (xhr.readyState != 4) return
        clearTimeout(xhrTimeout)
        if (xhr.status == 200) {
            var cert = JSON.parse(xhr.responseText);
            if (cert.SerialNumber == null) {
                document.getElementById("cert_txt_verify").setAttribute('style', 'visibility: hidden;');
                document.getElementById("message_verify").innerHTML = "��������� �� �������: <b>" + cert.Message + "<b>";
                document.getElementById("cert_verify_info").hidden = true;
            }
            else {
                document.getElementById("cert_txt_verify").setAttribute('style', 'visibility: visible;');
                document.getElementById("cert_verify_info").style.display = '';
                document.getElementById("cert_verify_info").hidden = false;
                document.getElementById("subject_verify").innerHTML = "��������: <b>" + cert.SubjectName + "<b>";
                document.getElementById("issuer_verify").innerHTML = "��������: <b>" + cert.IssuerName + "<b>";
                document.getElementById("algorithm_verify").innerHTML = "��������: <b>" + cert.Algorithm + "<b>";
                document.getElementById("notafter_verify").innerHTML = "������������ ��: <b>" + cert.NotAfter + "<b>";
                document.getElementById("serial_number_verify").innerHTML = "�������� �����: <b>" + cert.SerialNumber + "<b>";
                document.getElementById("message_verify").innerHTML = "��������� �� �������: <b>" + cert.Message + "<b>";
            }
        } else {
            handleError(xhr.statusText)
        }
    }
    xhr.send(data);
    var xhrTimeout = setTimeout(function () { xhr.abort(); handleError("Timeout") }, 10000);

    function handleError(message) {
        document.getElementById("cert_txt_verify").setAttribute('style', 'visibility: hidden;');
        document.getElementById("message_verify").innerHTML = "������ ��� ������� ����� � ��������";
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
    return this.extract(this.cert.SubjectName, 'CN=') + "; �����: " + this.GetCertFromDate();
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

