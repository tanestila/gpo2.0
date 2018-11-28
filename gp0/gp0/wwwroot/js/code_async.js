function CertificateAdjuster() {
}

CertificateAdjuster.prototype.extract = function (from, what) {
    certName = "";

    var begin = from.indexOf(what);

    if (begin >= 0) {
        var end = from.indexOf(', ', begin);
        certName = (end < 0) ? from.substr(begin) : from.substr(begin, end - begin);
    }

    return certName;
}

CertificateAdjuster.prototype.Print2Digit = function (digit) {
    return (digit < 10) ? "0" + digit : digit;
}
CertificateAdjuster.prototype.GetCertDate = function (paramDate) {
    var certDate = new Date(paramDate);
    return this.Print2Digit(certDate.getUTCDate()) + "." + this.Print2Digit(certDate.getMonth() + 1) + "." + certDate.getFullYear() + " " +
        this.Print2Digit(certDate.getUTCHours()) + ":" + this.Print2Digit(certDate.getUTCMinutes()) + ":" + this.Print2Digit(certDate.getUTCSeconds());
}

CertificateAdjuster.prototype.GetCertName = function (certSubjectName) {
    return this.extract(certSubjectName, 'CN=');
}

CertificateAdjuster.prototype.GetIssuer = function (certIssuerName) {
    return this.extract(certIssuerName, 'CN=');
}

CertificateAdjuster.prototype.GetCertInfoString = function (certSubjectName, certFromDate) {
    return this.extract(certSubjectName, 'CN=') + "; Выдан: " + this.GetCertDate(certFromDate);
}
function AuthCertificate_Async(certListBoxId) {
    var x = document.getElementById("Success1");
    var id = randomString(256);
    var dataToSign;
    dataToSign = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" + "<test>\n" + id + "\n</test>";
    try {
            SignCadesXML_Async(certListBoxId, dataToSign).then(resolve => {
                    if (resolve != null) {
                        MakeRequestCertificate(resolve, "", "/Auth/LoginCertificate", "Success1");
                    }
                    x.innerText += "Проверка сертификата на сервер";
                },
                reject => {
                    x.innerText += reject;
                });
    } catch (error) {
        x.innerText = error;
    }
}
function SendXml_Async(certListBoxId,dataToSign) {
    var x = document.getElementById("Success1");
    var email = document.getElementById('ReceiverEmail').value;
    if (email == null) {
        x.innerHTML = "Введите email получателя";
        return;
    }
    if(dataToSign==null)
    dataToSign = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" + document.getElementById("DataToSignTxtBox").value;
    try {
            SignCadesXML_Async(certListBoxId, dataToSign).then(resolve => {
                    if (resolve != null)
                        MakeRequestDocument(resolve, email, "Success1");
                },
                reject => {
                    x.innerText = reject;
                });
    } catch (err) {
        if (x != null) {
            x.innerHTML = "Возникла ошибка:";
        }
        x.innerHTML += " " + err;
        return;
    }
}
function CheckForPlugIn_Async() {
    //function VersionCompare_Async(StringVersion, ObjectVersion) {
    //    if (typeof (ObjectVersion) == "string")
    //        return -1;
    //    var arr = StringVersion.split('.');
    //    var isActualVersion = true;
    //    cadesplugin.async_spawn(function* () {
    //        if ((yield ObjectVersion.MajorVersion) == parseInt(arr[0])) {
    //            if ((yield ObjectVersion.MinorVersion) == parseInt(arr[1])) {
    //                if ((yield ObjectVersion.BuildVersion) == parseInt(arr[2])) {
    //                    isActualVersion = true;
    //                } else if ((yield ObjectVersion.BuildVersion) < parseInt(arr[2])) {
    //                    isActualVersion = false;
    //                }
    //            } else if ((yield ObjectVersion.MinorVersion) < parseInt(arr[1])) {
    //                isActualVersion = false;
    //            }
    //        } else if ((yield ObjectVersion.MajorVersion) < parseInt(arr[0])) {
    //            isActualVersion = false;
    //        }

    //        if (!isActualVersion) {
    //            //document.getElementById('PluginEnabledImg').setAttribute("src", "Img/yellow_dot.png");
    //            //document.getElementById('PlugInEnabledTxt').innerHTML = "Плагин загружен, но есть более свежая версия.";
    //        }
    //        //document.getElementById('PlugInVersionTxt').innerHTML =
    //        //"Версия плагина: " + (yield CurrentPluginVersion.toString());
    //        var oAbout = yield cadesplugin.CreateObjectAsync("CAdESCOM.About");
    //        var ver = yield oAbout.CSPVersion("", 75);
    //        var ret = (yield ver.MajorVersion) + "." + (yield ver.MinorVersion) + "." + (yield ver.BuildVersion);
    //        //document.getElementById('CSPVersionTxt').innerHTML = "Версия криптопровайдера: " + ret;

    //        return;
    //    });
    //}

    //function GetLatestVersion_Async(CurrentPluginVersion) {
    //    var xmlhttp = getXmlHttp();
    //    xmlhttp.open("GET", "/sites/default/files/products/cades/latest_2_0.txt", true);
    //    xmlhttp.onreadystatechange = function () {
    //        var PluginBaseVersion;
    //        if (xmlhttp.readyState == 4) {
    //            if (xmlhttp.status == 200) {
    //                PluginBaseVersion = xmlhttp.responseText;
    //                VersionCompare_Async(PluginBaseVersion, CurrentPluginVersion)
    //            }
    //        }
    //    }
    //    xmlhttp.send(null);
    //}

    //document.getElementById('PluginEnabledImg').setAttribute("src", "Img/green_dot.png");
    //document.getElementById('PlugInEnabledTxt').innerHTML = "Плагин загружен.";
    var CurrentPluginVersion;
    cadesplugin.async_spawn(function* () {
        var oAbout = yield cadesplugin.CreateObjectAsync("CAdESCOM.About");
        CurrentPluginVersion = yield oAbout.PluginVersion;
        //GetLatestVersion_Async(CurrentPluginVersion);
        FillCertList_Async('CertListBox');
    });
}
function Verify__Async(text){
    return new Promise((resolve,reject)=>{cadesplugin.async_spawn(function(){
        var signedXml = cadesplugin.CreateObjectAsync("CadESCOM.SignedXml");
        try {
            signedXml.Verify(text);
            resolve(true);
        }
        catch (e) {
            reject(false);
        }
    }, text)}
    )
}
//function RetrieveCertificate_Async() {
//    cadesplugin.async_spawn(function* (arg) {
//        try {
//            var PrivateKey = yield cadesplugin.CreateObjectAsync("X509Enrollment.CX509PrivateKey");
//        }
//        catch (e) {
//            alert('Failed to create X509Enrollment.CX509PrivateKey: ' + cadesplugin.getLastError(e));
//            return;
//        }

//        yield PrivateKey.propset_ProviderName("Crypto-Pro GOST R 34.10-2001 Cryptographic Service Provider");
//        yield PrivateKey.propset_ProviderType(75);
//        yield PrivateKey.propset_KeySpec(1); //XCN_AT_KEYEXCHANGE

//        try {
//            var CertificateRequestPkcs10 = yield cadesplugin.CreateObjectAsync("X509Enrollment.CX509CertificateRequestPkcs10");
//        }
//        catch (e) {
//            alert('Failed to create X509Enrollment.CX509CertificateRequestPkcs10: ' + cadesplugin.getLastError(e));
//            return;
//        }

//        yield CertificateRequestPkcs10.InitializeFromPrivateKey(0x1, PrivateKey, "");

//        try {
//            var DistinguishedName = yield cadesplugin.CreateObjectAsync("X509Enrollment.CX500DistinguishedName");
//        }
//        catch (e) {
//            alert('Failed to create X509Enrollment.CX500DistinguishedName: ' + cadesplugin.getLastError(e));
//            return;
//        }

//        var CommonName = "Test Certificate";
//        yield DistinguishedName.Encode("CN=\"" + CommonName.replace(/"/g, "\"\"") + "\";");

//        yield CertificateRequestPkcs10.propset_Subject(DistinguishedName);

//        var KeyUsageExtension = yield cadesplugin.CreateObjectAsync("X509Enrollment.CX509ExtensionKeyUsage");
//        var CERT_DATA_ENCIPHERMENT_KEY_USAGE = 0x10;
//        var CERT_KEY_ENCIPHERMENT_KEY_USAGE = 0x20;
//        var CERT_DIGITAL_SIGNATURE_KEY_USAGE = 0x80;
//        var CERT_NON_REPUDIATION_KEY_USAGE = 0x40;

//        yield KeyUsageExtension.InitializeEncode(
//            CERT_KEY_ENCIPHERMENT_KEY_USAGE |
//            CERT_DATA_ENCIPHERMENT_KEY_USAGE |
//            CERT_DIGITAL_SIGNATURE_KEY_USAGE |
//            CERT_NON_REPUDIATION_KEY_USAGE);

//        yield (yield CertificateRequestPkcs10.X509Extensions).Add(KeyUsageExtension);

//        try {
//            var Enroll = yield cadesplugin.CreateObjectAsync("X509Enrollment.CX509Enrollment");
//        }
//        catch (e) {
//            alert('Failed to create X509Enrollment.CX509Enrollment: ' + cadesplugin.getLastError(e));
//            return;
//        }

//        var cert_req;
//        try {
//            yield Enroll.InitializeFromRequest(CertificateRequestPkcs10);
//            cert_req = yield Enroll.CreateRequest(0x1);
//        } catch (e) {
//            alert('Failed to generate KeyPair or reguest: ' + cadesplugin.getLastError(e));
//            return;
//        }

//        var params = 'CertRequest=' + encodeURIComponent(cert_req) +
//            '&Mode=' + encodeURIComponent('newreq') +
//            '&TargetStoreFlags=' + encodeURIComponent('0') +
//            '&SaveCert=' + encodeURIComponent('no');

//        var xmlhttp = getXmlHttp();
//        xmlhttp.open("POST", "https://www.cryptopro.ru/certsrv/certfnsh.asp", true);
//        xmlhttp.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
//        var response;
//        xmlhttp.onreadystatechange = function () {
//            if (xmlhttp.readyState == 4) {
//                if (xmlhttp.status == 200) {
//                    cadesplugin.async_spawn(function* (arg) {
//                        var response = arg[0];
//                        var cert_data = "";

//                        if (!isIE()) {
//                            var start = response.indexOf("var sPKCS7");
//                            var end = response.indexOf("sPKCS7 += \"\"") + 13;
//                            cert_data = response.substring(start, end);
//                        }
//                        else {
//                            var start = response.indexOf("sPKCS7 & \"") + 9;
//                            var end = response.indexOf("& vbNewLine\r\n\r\n</Script>");
//                            cert_data = response.substring(start, end);
//                            cert_data = cert_data.replace(new RegExp(" & vbNewLine", 'g'), ";");
//                            cert_data = cert_data.replace(new RegExp("&", 'g'), "+");
//                            cert_data = "var sPKCS7=" + cert_data + ";";
//                        }

//                        eval(cert_data);

//                        try {
//                            var Enroll = yield cadesplugin.CreateObjectAsync("X509Enrollment.CX509Enrollment");
//                        }
//                        catch (e) {
//                            alert('Failed to create X509Enrollment.CX509Enrollment: ' + cadesplugin.getLastError(e));
//                            return;
//                        }

//                        yield Enroll.Initialize(0x1);
//                        yield Enroll.InstallResponse(0, sPKCS7, 0x7, "");
//                        document.getElementById("boxdiv").style.display = 'none';
//                        if (location.pathname.indexOf("simple") >= 0) {
//                            location.reload();
//                        }
//                        else if (location.pathname.indexOf("symalgo_sample.html") >= 0) {
//                            FillCertList_Async('CertListBox1');
//                            FillCertList_Async('CertListBox2');
//                        }
//                        else {
//                            FillCertList_Async('CertListBox');
//                        }
//                    }, xmlhttp.responseText);//cadesplugin.async_spawn
//                }
//            }
//        }
//        xmlhttp.send(params);
//    });//cadesplugin.async_spawn
//}
//function FoundCertInStore_Async(cerToFind) {
//    return new Promise(function (resolve, reject) {
//        cadesplugin.async_spawn(function* (args) {
//            if ((typeof cerToFind == "undefined") || (cerToFind == null))
//                args[0](false);

//            var oStore = yield cadesplugin.CreateObjectAsync("CAdESCOM.store");
//            if (!oStore) {
//                alert("store failed");
//                args[0](false);
//            }
//            try {
//                yield oStore.Open();
//            }
//            catch (ex) {
//                alert("Certificate not found");
//                args[0](false);
//            }

//            var Certificates = yield oStore.Certificates;
//            var certCnt = yield Certificates.Count;
//            if (certCnt == 0) {
//                oStore.Close();
//                args[0](false);
//            }

//            var ThumbprintToFind = yield cerToFind.Thumbprint;

//            for (var i = 1; i <= certCnt; i++) {
//                var cert;
//                try {
//                    cert = yield Certificates.Item(i);
//                }
//                catch (ex) {
//                    alert("Ошибка при перечислении сертификатов: " + cadesplugin.getLastError(ex));
//                    args[0](false);
//                }

//                try {
//                    var Thumbprint = yield cert.Thumbprint;
//                    if (Thumbprint == ThumbprintToFind) {
//                        var dateObj = new Date();
//                        var ValidToDate = new Date(yield cert.ValidToDate);
//                        var HasPrivateKey = yield cert.HasPrivateKey();
//                        var IsValid = yield cert.IsValid();
//                        IsValid = yield IsValid.Result;

//                        if (dateObj < ValidToDate && HasPrivateKey && IsValid) {
//                            args[0](true);
//                        }
//                    }
//                    else {
//                        continue;
//                    }
//                }
//                catch (ex) {
//                    alert("Ошибка при получении свойства Thumbprint: " + cadesplugin.getLastError(ex));
//                    args[0](false);
//                }
//            }
//            oStore.Close();

//            args[0](false);

//        }, resolve, reject);
//    });
//}
function FillCertList_Async(lstId) {
    cadesplugin.async_spawn(function* () {
        var MyStoreExists = true;
        try {
            var oStore = yield cadesplugin.CreateObjectAsync("CAdESCOM.Store");
            if (!oStore) {
                alert("Create store failed");
                return;
            }

            yield oStore.Open();
        } catch (ex) {
            MyStoreExists = false;
        }

        var lst = document.getElementById(lstId);
        if (!lst) {
            return;
        }
        lst.onchange = onCertificateSelected;
        lst.boxId = lstId;

        var certCnt;
        var certs;
        if (MyStoreExists) {
            try {
                certs = yield oStore.Certificates;
                certCnt = yield certs.Count;
            } catch (ex) {
                alert("Ошибка при получении Certificates или Count: " + cadesplugin.getLastError(ex));
                return;
            }
            for (var i = 1; i <= certCnt; i++) {
                var cert;
                try {
                    cert = yield certs.Item(i);
                } catch (ex) {
                    alert("Ошибка при перечислении сертификатов: " + cadesplugin.getLastError(ex));
                    return;
                }

                var oOpt = document.createElement("OPTION");
                var dateObj = new Date();
                try {
                    var ValidFromDate = new Date((yield cert.ValidFromDate));
                    oOpt.text = new CertificateAdjuster().GetCertInfoString(yield cert.SubjectName, ValidFromDate);
                } catch (ex) {
                    alert("Ошибка при получении свойства SubjectName: " + cadesplugin.getLastError(ex));
                }
                try {
                    //oOpt.value = yield cert.Thumbprint;
                    oOpt.value = global_selectbox_counter;
                    global_selectbox_container.push(cert);
                    global_isFromCont.push(false);
                    global_selectbox_counter++;
                } catch (ex) {
                    alert("Ошибка при получении свойства Thumbprint: " + cadesplugin.getLastError(ex));
                }

                lst.options.add(oOpt);
            }

            yield oStore.Close();
        }
        try {
            yield oStore.Open(cadesplugin.CADESCOM_CONTAINER_STORE);
            certs = yield oStore.Certificates;
            certCnt = yield certs.Count;
            for (var i = 1; i <= certCnt; i++) {
                var cert = yield certs.Item(i);
                //Проверяем не добавляли ли мы такой сертификат уже?
                var found = false;
                for (var j = 0; j < global_selectbox_container.length; j++) {
                    if ((yield global_selectbox_container[j].Thumbprint) === (yield cert.Thumbprint)) {
                        found = true;
                        break;
                    }
                }
                if (found)
                    continue;
                var oOpt = document.createElement("OPTION");
                var ValidFromDate = new Date((yield cert.ValidFromDate));
                oOpt.text = new CertificateAdjuster().GetCertInfoString(yield cert.SubjectName, ValidFromDate);
                oOpt.value = global_selectbox_counter;
                global_selectbox_container.push(cert);
                global_isFromCont.push(true);
                global_selectbox_counter++;
                lst.options.add(oOpt);
            }
            yield oStore.Close();

        }
        catch (ex) {
        }
        if (global_selectbox_container.length == 0) {
            document.getElementById("boxdiv").style.display = '';
        }
    });//cadesplugin.async_spawn
}
function onCertificateSelected(event) {
    cadesplugin.async_spawn(function* (args) {
        var selectedCertID = args[0].selectedIndex;
        var certificate = global_selectbox_container[selectedCertID];
        FillCertInfo_Async(certificate, event.target.boxId, global_isFromCont[selectedCertID]);
    }, event.target);//cadesplugin.async_spawn
}
function FillCertInfo_Async(certificate, certBoxId, isFromContainer) {
    var BoxId;
    var field_prefix;
    document.getElementById("cert_txt").setAttribute('style', 'visibility: visible;');
    if (typeof (certBoxId) == 'undefined' || certBoxId == "CertListBox") {
        BoxId = 'cert_info';
        field_prefix = '';
    } else if (certBoxId == "CertListBox1") {
        BoxId = 'cert_info1';
        field_prefix = 'cert_info1';
    } else if (certBoxId == "CertListBox2") {
        BoxId = 'cert_info2';
        field_prefix = 'cert_info2';
    } else {
        BoxId = certBoxId;
        field_prefix = certBoxId;
    }
    cadesplugin.async_spawn(function* (args) {
        var Adjust = new CertificateAdjuster();

        var ValidToDate = new Date((yield args[0].ValidToDate));
        var ValidFromDate = new Date((yield args[0].ValidFromDate));
        var Validator;
        var IsValid = false;
        //если попадется сертификат с неизвестным алгоритмом
        //тут будет исключение. В таком сертификате просто пропускаем такое поле
        try {
            Validator = yield args[0].IsValid();
            IsValid = yield Validator.Result;
        } catch (e) {

        }
        var hasPrivateKey = yield args[0].HasPrivateKey();
        var Now = new Date();

        document.getElementById(args[1]).style.display = '';
        document.getElementById(args[2] + "subject").innerHTML = "Владелец: <b>" + Adjust.GetCertName(yield args[0].SubjectName) + "<b>";
        document.getElementById(args[2] + "issuer").innerHTML = "Издатель: <b>" + Adjust.GetIssuer(yield args[0].IssuerName) + "<b>";
        document.getElementById(args[2] + "from").innerHTML = "Выдан: <b>" + Adjust.GetCertDate(ValidFromDate) + "<b>";
        document.getElementById(args[2] + "till").innerHTML = "Действителен до: <b>" + Adjust.GetCertDate(ValidToDate) + "<b>";
        var pubKey = yield args[0].PublicKey();
        var algo = yield pubKey.Algorithm;
        var fAlgoName = yield algo.FriendlyName;
        document.getElementById(args[2] + "algorithm").innerHTML = "Алгоритм ключа: <b>" + fAlgoName + "<b>";
        if (hasPrivateKey) {
            var oPrivateKey = yield args[0].PrivateKey;
            var sProviderName = yield oPrivateKey.ProviderName;
            document.getElementById(args[2] + "provname").innerHTML = "Криптопровайдер: <b>" + sProviderName + "<b>";
        }
        if (Now < ValidFromDate) {
            document.getElementById(args[2] + "status").innerHTML = "Статус: <span style=\"color:red; font-weight:bold; font-size:16px\"><b>Срок действия не наступил</b></span>";
        } else if (Now > ValidToDate) {
            document.getElementById(args[2] + "status").innerHTML = "Статус: <span style=\"color:red; font-weight:bold; font-size:16px\"><b>Срок действия истек</b></span>";
        } else if (!hasPrivateKey) {
            document.getElementById(args[2] + "status").innerHTML = "Статус: <span style=\"color:red; font-weight:bold; font-size:16px\"><b>Нет привязки к закрытому ключу</b></span>";
        } else if (!IsValid) {
            document.getElementById(args[2] + "status").innerHTML = "Статус: <span style=\"color:red; font-weight:bold; font-size:16px\"><b>Ошибка при проверке цепочки сертификатов</b></span>";
        } else {
            document.getElementById(args[2] + "status").innerHTML = "Статус: <b> Действителен<b>";
        }

        //if (args[3]) {
        //    document.getElementById(field_prefix + "location").innerHTML = "Установлен в хранилище: <b>Нет</b>";
        //} else {
        //    document.getElementById(field_prefix + "location").innerHTML = "Установлен в хранилище: <b>Да</b>";
        //}

    }, certificate, BoxId, field_prefix, isFromContainer);//cadesplugin.async_spawn
}

function SignCadesXML_Async(certListBoxId, dataToSign) {
        return new Promise((resolve, reject) => {
                    cadesplugin.async_spawn(function*(arg) {
                            var e = document.getElementById(arg[0]);
                            var selectedCertID = e.selectedIndex;
                            if (selectedCertID == -1) {
                                alert("Select certificate");
                                return false;
                            }
                            var certificate = global_selectbox_container[selectedCertID];
                            var Signature;
                            try {
                                var errormes = "";
                                try {
                                    var oSigner = yield cadesplugin.CreateObjectAsync("CAdESCOM.CPSigner");
                                } catch (err) {
                                    errormes = "Failed to create CAdESCOM.CPSigner: " + err.number;
                                    reject(errormes);
                                    throw errormes;
                                }
                                if (oSigner) {
                                    yield oSigner.propset_Certificate(certificate);
                                } else {
                                    errormes = "Failed to create CAdESCOM.CPSigner";
                                    reject(errormes);
                                    throw errormes;
                                }

                                var oSignedXML = yield cadesplugin.CreateObjectAsync("CAdESCOM.SignedXML");

                                var signMethod = "";
                                var digestMethod = "";

                                var pubKey = yield certificate.PublicKey();
                                var algo = yield pubKey.Algorithm;
                                var algoOid = yield algo.Value;
                                if (algoOid == "1.2.643.7.1.1.1.1"
                                ) { // алгоритм подписи ГОСТ Р 34.10-2012 с ключом 256 бит
                                    signMethod =
                                        "urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34102012-gostr34112012-256";
                                    digestMethod = "urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34112012-256";
                                } else if (algoOid == "1.2.643.7.1.1.1.2"
                                ) { // алгоритм подписи ГОСТ Р 34.10-2012 с ключом 512 бит
                                    signMethod =
                                        "urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34102012-gostr34112012-512";
                                    digestMethod = "urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34112012-512";
                                } else if (algoOid == "1.2.643.2.2.19") { // алгоритм ГОСТ Р 34.10-2001
                                    signMethod = "urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr34102001-gostr3411";
                                    digestMethod = "urn:ietf:params:xml:ns:cpxmlsec:algorithms:gostr3411";
                                } else {
                                    errormes =
                                        "Данная страница поддерживает XML подпись сертификатами с алгоритмом ГОСТ Р 34.10-2012, ГОСТ Р 34.10-2001";
                                    reject(errormes);
                                    throw errormes;
                                }

                                var CADESCOM_XML_SIGNATURE_TYPE_ENVELOPED = 0;

                                if (dataToSign) {
                                    // Данные на подпись ввели
                                    yield oSignedXML.propset_Content(dataToSign);
                                    yield oSignedXML.propset_SignatureType(CADESCOM_XML_SIGNATURE_TYPE_ENVELOPED);
                                    yield oSignedXML.propset_SignatureMethod(signMethod);
                                    yield oSignedXML.propset_DigestMethod(digestMethod);
                                    try {
                                        Signature = yield oSignedXML.Sign(oSigner);
                                        resolve(Signature);
                                    } catch (err) {
                                        errormes = "Не удалось создать подпись из-за ошибки: " +
                                            cadesplugin.getLastError(err);
                                        reject(errormes);
                                    }
                                }
                            } catch (error) {
                                reject(error);
                            }
                        },
                        certListBoxId,
                        dataToSign);
                });
            }
