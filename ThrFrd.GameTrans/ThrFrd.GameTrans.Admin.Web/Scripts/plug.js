var plug = (function () {
    //private
    PlugInit = function () { if (typeof (jQuery) == 'undefined') { alert('请加载jQuery库'); return false; } return true; }
    getElementHeight = function (obj) {
        return jQuery(obj).height();
    };
    getElementWidth = function (obj) {
        return jQuery(obj).width();
    };
    getWindowCurrentHeight = function () {
        return jQuery(window).height();
    };
    getWindowCurrentWidth = function () {
        return jQuery(window).width();
    };
    getWindowMaxWidth = function () {
        return Math.max(jQuery(document.body).width(), jQuery(document).scroll().width());
    };
    getWindowMaxHeight = function () {
        return Math.max(jQuery(document.body).height(), jQuery(document).scroll().height());
    };
    getScrollTop = function () {
        return jQuery(document).scrollTop();
    };
    getScrollLeft = function () {
        return jQuery(document).scrollLeft()
    };
    return {
        BindValidCode: (function () {
            var rad = Math.random();
            var interval = null;
            var time = 120;
            ReflashTokenForLogin = function () {
                rad = Math.random();
                jQuery('input[name="token"]').val(rad);
            };
            setValidtorTimeOut = function () {
                if (interval == null) {
                    interval = window.setInterval(function () {
                        var s = parseInt(time, 10);
                        if (s > 0) {
                            time -= 1;
                        }
                        else {
                            jQuery('#ValidImg').trigger('click');
                        }
                    }, 1000);
                }
            }
            return function () {
                this.BindValidator = function () {
                    jQuery(function () {
                        ReflashTokenForLogin();
                        jQuery('#codeimg').html('<img  id="ValidImg" src="/account/validatetoken?t=' + rad + '" alt="点击刷新" onclick="Refalsh(this);" style="width:80px;height:30px;margin-top:-10px;cursor:pointer;"/>');
                        setValidtorTimeOut();
                    });
                }
                this.Refalsh = function (sender) {
                    ReflashTokenForLogin();
                    time = 120;
                    //jQuery('#Validator').val('');
                    jQuery(sender).attr('src', '/Account/ValidateToken?t=' + rad);

                };
            };
        })(),
        //summary：
        //e.g: <input type="text" av="{x:'$email',t:'tipid',m:'email格式'}" id="txt"></input>
        AValidator: (function () {
            var reglib = {
                email: '^\\w+((-\\w+)|(\\.\\w+))*\\@[A-Za-z0-9]+((\\.|-)[A-Za-z0-9]+)*\\.[A-Za-z0-9]+$',
                date: '^(?:(?:1[6-9]|[2-9][0-9])[0-9]{2}([-/.]?)(?:(?:0?[1-9]|1[0-2])\\1(?:0?[1-9]|1[0-9]|2[0-8])|(?:0?[13-9]|1[0-2])\\1(?:29|30)|(?:0?[13578]|1[02])\\1(?:31))|(?:(?:1[6-9]|[2-9][0-9])(?:0[48]|[2468][048]|[13579][26])|(?:16|[2468][048]|[3579][26])00)([-/.]?)0?2\\2(?:29))$',
                phone: '^(\\d{3}\\-\\d{8}$|^\\d{4}\\-\\d{7})$|^(\\d{11})$',
                async: false,
                ajax: {
                    username: function (username, onsuccess) {
                        $.ajax({
                            url: '/account/usernameonly',
                            type: 'get',
                            //async: false,
                            data: { username: username },
                            success: function (result) {
                                onsuccess(result ? 0 : 1);
                            }
                        });
                    }
                }
            };
            setup = function (formid) {
                if ($('#' + formid).length > 0) {
                    $('#' + formid + ' [av]').each(function () {
                        $(this).blur(function () {
                            doValidate($(this));
                        });
                    });
                    $('#' + formid).submit(function () {
                        var isPass = true;
                        for (var i = 0, len = $('#' + formid + ' [av]').length; i < len; i++) {
                            if (!doValidate($('#' + formid + ' [av]').eq(i))) {
                                isPass = false;
                                break;
                            }
                        }
                        //                        $('#' + formid + ' [av]').each(function () {
                        //                            if (!doValidate($(this))) {
                        //                                if (isPass) {
                        //                                    isPass = false;
                        //                                    return false;
                        //                                }
                        //                            }
                        //                        });
                        return isPass;
                    });
                }
            };
            getattribute = function (element) {
                if (element) {
                    var param = {};
                    var str = element.attr('av');
                    try {
                        var prop = eval('(' + str + ')');
                        if (prop) {
                            var regex = prop.x;
                            if (regex) {
                                if (regex.substring(0, 1) == ':') {
                                    //正则表达式方式
                                    param.regex = regex.substring(1);
                                }
                                else if (regex.substring(0, 1) == '$') {
                                    //预定义正则表达式方式
                                    var name = regex.substring(1);
                                    param.regex = eval('reglib.' + name);
                                }
                                else if (regex.substring(0, 1) == '=') {
                                    //值对比方式
                                    var targetid = regex.substring(1);
                                    param.regex = '^' + $('#' + targetid).val() + '$';
                                }
                                else if (regex.substring(0, 1) == '!') {
                                    //必须checked="checked"
                                    param.regex = "checked";
                                }
                            }
                            if (prop.ajax) {
                                param.ajax = prop.ajax;
                            }
                            if (prop.a) {
                                param.action = prop.a;
                            }
                            param.msg = prop.m;
                            param.tip = prop.t;
                        }
                    }
                    catch (exp) {
                        return undefined;
                    }
                    return param;
                }
            };
            doValidate = function (element) {
                var param = getattribute(element);
                var tip = param.tip;
                if (param) {
                    var np = false;
                    if (param.regex == 'checked') {
                        np = !element.attr('checked');
                    }
                    else {
                        np = !eval('/' + param.regex + '/ig.test("' + element.val() + '")');
                    }
                    if (np) {
                        if (tip && $('#' + tip).length > 0) {
                            switch (param.action) {
                                case 'show':
                                    $('#' + tip).show();
                                    $('#' + tip).text(param.msg);
                                    break;
                                case 'append':
                                    $('#' + tip).append(param.msg);
                                    break;
                                default:
                                    $('#' + tip).text(param.msg);
                                    break;
                            }
                        }
                        return false;
                    }
                    else {
                        switch (param.action) {
                            case 'show':
                                $('#' + tip).hide();
                                $('#' + tip).text('');
                                break;
                            case 'append':
                                $('#' + tip).text($('#' + tip).text().replace(param.msg, ''));
                                break;
                            default:
                                $('#' + tip).text('');
                                break;
                        }
                    }
                    if (param.ajax) {
                        var func = eval('reglib.ajax.' + param.ajax.replace('$', ''));
                        if (func) {
                            var ispass = true;
                            func(element.val(), function (result) {
                                if (result == '1') {
                                    var tip = param.tip;
                                    if (tip && $('#' + tip).length > 0) {
                                        $('#' + tip).show();
                                        $('#' + tip).text('用户名重复');
                                    }
                                    ispass = false;
                                }
                                else {
                                    $('#' + tip).hide();
                                    $('#' + tip).text('');
                                }
                            });
                            return ispass;
                        }
                    }
                    return true;
                }
                return false;
            };
            return function () {
                this.BuildValidate = function (formid) {
                    setup(formid);
                }
            }
        })(),
        //public
        //summary：给input元素添加默认提示文字
        ValueSkin: (function () {
            return function () {
                this.Register = function () {
                    $('input[default]').each(function () {
                        if ($(this).attr('type') == 'text') {
                            if ($(this).val().trim() == '') {
                                $(this).css({ 'color': '#999', 'font-style': 'italic' })
                                    .val($(this).attr('default'))
                                    .blur(function () {
                                        if ($(this).val().trim() == '') {
                                            $(this)
                                                .css({ 'color': '#999', 'font-style': 'italic' })
                                                .val($(this).attr('default'));
                                        }
                                    })
                                    .focus(function () {
                                        if ($(this).val().trim() == $(this).attr('default')) {
                                            $(this).val('').css({ 'color': '#000', 'font-style': 'normal' });
                                        }
                                    });
                            }
                        }
                    });
                }
            }
        })(),
        /*End ValueSkin Method**************************************/

        //summary：模态弹出层类
        PopFrame: (function () {
            //private
            return function () {
                //public
                /**************************************
                * summary：展现弹出层
                * param[poplayerid]:需要弹出的层id
                * param[closebtnid]:关闭该弹出层的按钮*/
                var hidselectList = [];
                var _width = -1; //获取弹出层真实宽度
                var _height = -1;  //获取弹出层真实高度
                this.OpenFrame = function (poplayerid, closebtnid, onloaded, onclose) {
                    //if (jQuery.browser.version == '6.0') {
                    //    jQuery('select').each(function () {
                    //        if (jQuery(this).css('display') != 'none') {
                    //            hidselectList.push(jQuery(this));
                    //            jQuery(this).hide();
                    //        }
                    //    });
                    //}

                    if (!PlugInit()) return;
                    var poplayer = jQuery('#' + poplayerid); //获取弹出层
                    if (poplayer == undefined) {
                        alert('无法获取到弹出层，这可能是dom元素未加载完成。');
                        return;
                    }
                    var background = "ipopbackground"; //新建遮罩层id
                    //if (_width == -1 || _height == -1) {
                    _width = getElementWidth(poplayer);  //获取弹出层真实宽度
                    _height = getElementHeight(poplayer); //获取弹出层真实高度
                    //}
                    if (poplayer.length > 0) jQuery(poplayer).remove(); //移除页面上元素
                    if (jQuery('#' + background).length > 0) jQuery('#' + background).remove(); //移除页面上元素 

                    //遮罩层
                    var newMask = jQuery('<div id=\'' + background + '\'/>');
                    newMask.css({
                        position: 'absolute',
                        zIndex: '10',
                        width: getWindowMaxWidth() + 'px',
                        height: getWindowMaxHeight() + 'px',
                        top: '0px',
                        left: '0px',
                        background: '#ccc',
                        filter: 'alpha(opacity=40)',
                        opacity: '0'
                    });
                    jQuery('body').append(newMask);
                    //新弹出层
                    var newDiv = jQuery('<div id=\'' + poplayerid + '\'/>');
                    newDiv.css({
                        display: 'block',
                        position: 'absolute',
                        zIndex: '99',
                        width: _width + 'px',
                        height: _height + 'px',
                        top: parseInt(getScrollTop() + getWindowCurrentHeight() / 2 - _height / 2, 10) + "px",
                        left: parseInt(getScrollLeft() + getWindowCurrentWidth() / 2 - _width / 2, 10) + "px",
                        background: "#fff",
                        border: '8px solid #bbb',
                        padding: '5px'
                    });
                    jQuery('body').append(newDiv.html(poplayer.html()).hide());
                    newDiv.fadeIn(600, function () {
                        if (typeof (onloaded) == 'function') {
                            onloaded();
                        }
                    });

                    //绑定关闭事件
                    jQuery('#' + closebtnid).click(function () {
                        var box = jQuery(newDiv).fadeOut(600, function () {
                            jQuery(newMask).remove();
                            jQuery(newDiv).remove();
                            var length = hidselectList.length;
                            for (var i = 0; i < length; i++) {
                                hidselectList[i].show();
                            }
                            jQuery('body').append(newDiv);
                            if (typeof (onclose) == 'function') {
                                onclose();
                            }
                        }); //.hide();
                    });

                    //绑定滚动事件
                    jQuery(window).scroll(function () {
                        newDiv.css({
                            top: (getScrollTop() + getWindowCurrentHeight() / 2 - _height / 2) + "px",
                            left: (getScrollLeft() + getWindowCurrentWidth() / 2 - _width / 2) + "px"
                        });
                    });
                    //窗体大小变化事件
                    jQuery(window).resize(function (a) {
                        newMask.css({
                            width: getWindowMaxWidth() + 'px',
                            height: getWindowMaxHeight() + 'px'
                        });
                        newDiv.css({
                            top: (getScrollTop() + getWindowCurrentHeight() / 2 - _height / 2) + "px",
                            left: (getScrollLeft() + getWindowCurrentWidth() / 2 - _width / 2) + "px"
                        });
                    });
                }
            }
        })(),
        /*End PopFrame Method**************************************/

        //summary：html编码
        Security: (function () {
            return function () {
                this.HTMLTextEncode = function (input) {
                    return input.replace(/</gi, '&lt;').replace(/>/gi, '&gt;');
                }
            }
        })(),
        /*End Security Method**************************************/
        //summary：控件数据绑定
        Control: (function () {
            return function () {
                this.BuildRelationSelect = function (parent, child, datasource) {
                    if ((typeof datasource).toLowerCase() == 'string') {
                        datasource = data.GetDataSource(datasource);
                    }
                    var bs = this.BuildSelect;
                    var setting = {};
                    if ((typeof (parent)).toString().toLowerCase() == 'string') {
                        setting.element = parent;
                    }
                    else {
                        setting = $.extend(setting, parent);
                    }
                    setting.onchange = function () {
                        var sltValue = $(this).val();
                        var childElements; // = datasource[sltValue].children;
                        for (var i = 0, len = datasource.length; i < len; i++) {
                            if (datasource[i].value == sltValue) {
                                childElements = datasource[i].children;
                            }
                        }
                        if (childElements) {
                            bs(child, childElements);
                        }
                    };

                    var o = bs(setting, datasource);
                    o.trigger('change');
                },
                this.BuildSelect = function (setting, datasource) {
                    var is = {
                        nobind: [], //不绑定到控件的值列表，比如 值为0,1,2 的项不绑定出来。则值为[0,1,2]
                        bindtype: 'value', //给option的value属性绑定数据源的value还是name
                        create: false, //创建一个新的select元素
                        defaultvalue: null, //如果选择的值
                        onchange: null //发生选择时的事件
                        //element:'' 需要提供元素的id
                        //elementName:'' [如果是新创建元素]元素的name
                    };
                    //如果参数是字符串，则认为是对一个已存在的select进行元素绑定
                    if ((typeof (setting)).toString().toLowerCase() == 'string') {
                        is.element = setting;
                        setting = is;
                    }
                    else {
                        setting = $.extend(is, setting);
                    }
                    var o = null;
                    if (setting.create) {
                        o = $('<select/>').attr('id', setting.element).attr('name', setting.elementName);
                        if (setting.onchange != null) {
                            o.change(setting.onchange);
                        }
                    }
                    else {
                        //如果不是创建新的，则根据id选择
                        o = $('#' + setting.element);
                        o.empty();
                    }

                    if (o.length > 0) {
                        if ((typeof (datasource)).toString().toLowerCase() == 'string') {
                            datasource = data.GetDataSource(datasource);
                        }
                        for (var i = 0, len = datasource.length; i < len; i++) {
                            if (setting.nobind.contain(datasource[i].value)) {
                                continue;
                            }
                            var item = $('<option/>')
                                .text(datasource[i].name);
                            if (setting.bindtype == 'name') {
                                item.val(datasource[i].name);
                            }
                            else {
                                item.val(datasource[i].value);
                            }
                            if (setting.defaultvalue != null && setting.defaultvalue != '' && datasource[i].value == setting.defaultvalue) {
                                item.attr('selected', 'selected');
                            }
                            o.append(item);

                        }
                    }
                    if (is.onchange && (typeof (is.onchange)).toString().toLowerCase() == 'function') {
                        o.change(is.onchange);
                    }
                    return o;
                }
            };
        })(),
        BMap: (function () {
            var lng, lat;
            var location;
            var map, marker, myGeo;
            return function () {
                this.Design = function (container, address, location) {
                    this.location = location;
                    map = new BMap.Map(container);
                    map.enableScrollWheelZoom();
                    myGeo = new BMap.Geocoder();
                    myGeo.getPoint(location, function (point) {
                        if (point) {
                            lng = point.lng;
                            lat = point.lat;
                            map.centerAndZoom(point, 12);
                            map.addEventListener('rightclick', function (e) {
                                map.clearOverlays();
                                marker = new BMap.Marker(e.point);
                                marker.enableDragging();
                                lng = e.point.lng;
                                lat = e.point.lat;
                                map.addOverlay(marker);
                            });
                        }
                    }, location);
                },
				this.OpenMap = function (container, prop) {
				    map = new BMap.Map(container);
				    map.enableScrollWheelZoom();
				    if (prop.search) {
				        myGeo = new BMap.Geocoder();
				        myGeo.getPoint(prop.address, function (point) {
				            if (point) {
				                lng = point.lng;
				                lat = point.lat;
				                map.centerAndZoom(point, 13);
				                marker = new BMap.Marker(e.point);
				                marker.enableDragging();
				                lng = e.point.lng;
				                lat = e.point.lat;
				                map.addOverlay(marker);
				            }
				            else {
				                if (prop.error) {
				                    prop.error();
				                }
				            }
				        }, prop.location);
				    }
				    else {
				        var point = new BMap.Point(prop.lng, prop.lat);
				        map.centerAndZoom(point, prop.zoom);

				        marker = new BMap.Marker(point);
				        map.addOverlay(marker);
				    }
				},
				this.ClearMarker = function () {
				    map.clearOverlays();
				},
				this.CutMap = function (mustmark) {
				    if (mustmark) {
				        if (!marker) {
				            alert('请先通过鼠标右键标记您的位置！');
				            return;
				        }
				    }
				    return { zoom: map.getZoom(), lng: lng, lat: lat };
				}
            }
        })(),
        MessageBox: (function () {
            return function () {
                this.Show = function (html) {
                    if ($('#boxplugmsgboxcontainer').length == 0) {
                        var msgbox = $('<div id="boxplugmsgboxcontainer" style="display:none;width:300px;height:auto;padding:50px 10px 30px 10px;"><div style="width:280px;height:auto;">' + html + '</div><div style="width:85px;margin-left:auto;margin-right:auto;"><a href="javascript:;" id="btnplugmsgboxclose"><img src="/content/images/submit.png" alt="ok"/></a></div></div></div>');
                        $('body').append(msgbox);
                    }
                    var popbox = new plug.PopFrame();
                    popbox.OpenFrame('boxplugmsgboxcontainer', 'btnplugmsgboxclose', null);
                }
            }
        })(),
        SwfUpload: (function () {
            var def = {
                file_size_limit: "7 MB",
                file_types: "*.*",
                file_types_description: "All Files",
                file_upload_limit: 0,
                flash_url: "/scripts/swfupload/swfupload.swf",
                button_image_url: '/scripts/swfupload/btn_upload.png',
                button_width: 125,
                button_height: 40,
                button_placeholder_id: 'btnswfupload',
                /***********************************************/
                //当控件加载完成时
                onLoad: function (event) { },
                //当有一个新的问题被添加到上传队列时
                onAppend: function (event, file) { },
                //当文件开始上传时
                onBegin: function (event, file) { },
                //文件上传进度，周期性触发
                progress: function (event, file, bytesLoaded) { },
                //当文件上传完毕时
                onComplete: function (event, file) { },
                //问及上传成功
                onSuccess: function (event, file, serverData) { },
                //文件上传失败
                onError: function (event, file, errorCode, message) { },
                //当文件选择完毕时
                onSelected: function (event, numFilesSelected, numFilesQueued) { },
                //当控件选择文件被打开
                onDialogStart: function (event) { }
            };
            var config = {}, imgcache = [];
            function GetImages() {
                var str = '';
                for (var i = 0, len = imgcache.length; i < len; i++) {
                    str += imgcache[i].horizontal + ',' + imgcache[i].name + ',' + imgcache[i].path + ';';
                }
                return str;
            }
            return function () {
                this.Bind = function (container, setting) {
                    if ($('#' + container)) {
                        config = $.extend(def, setting);
                        $('#' + container).append($('<input type="button" id="btnswfupload" />'));
                        var control = $('#' + container).swfupload(config)
                        .bind('swfuploadLoaded', function (event) {
                            config.onLoad(event);
                        })
                        .bind('fileDialogStart', function (event) {
                            config.onDialogStart(event);
                        })
                        .bind('fileDialogComplete', function (event, numFilesSelected, numFilesQueued) {
                            config.onSelected(event, numFilesSelected, numFilesQueued);
                        })
                        .bind('fileQueued', function (event, file) {
                            config.onAppend(event, file);
                            $(this).swfupload('startUpload');
                        })
                        .bind('fileQueueError', function (event, file, errorCode, message) {
                            config.onError(event, file, errorCode, message);
                        })
                        .bind('uploadStart', function (event, file) {
                            config.onBegin(event, file);
                        })
                        .bind('uploadProgress', function (event, file, bytesLoaded) {
                            config.progress(event, file, bytesLoaded);
                        })
                        .bind('uploadSuccess', function (event, file, serverData) {
                            config.onSuccess(event, file, serverData);
                        })
                        .bind('uploadComplete', function (event, file) {
                            config.onComplete(event, file);
                            $(this).swfupload('startUpload');
                        })
                        .bind('uploadError', function (event, file, errorCode, message) {
                            config.onError(event, file, errorCode, message);
                        });
                        if (config.commitbtn) {
                            $('#' + config.commitbtn).click(function () {
                                if (imgcache.length > 0) {
                                    $.ajax({
                                        url: config.commiturl,
                                        data: { msg: config.commitdata, imgs: GetImages() },
                                        type: 'post',
                                        cache: false,
                                        success: function (result) {
                                            if (config.commitComplete) {
                                                config.commitComplete(eval('(' + result + ')'));
                                                imgcache = [];
                                                //config.commitComplete();
                                            }
                                        }
                                    });
                                }
                                else {
                                    if (config.commitComplete) {
                                        config.commitComplete([]);
                                        //config.commitComplete();
                                    }
                                }
                            });
                        }
                    }
                },
                this.PutImage = function (img) {
                    if (img) {
                        imgcache.push(img);
                    }
                },
                this.uninstall = function () {
                    for (var instance in SWFUpload.instances) {
                        if (SWFUpload.instances.hasOwnProperty(instance)) {
                            eval('SWFUpload.instances.' + instance.toString()).destroy();
                        }
                    }
                },
                this.DeleteItem = function (sender) {
                    var img = $(sender).siblings('div').children('img').attr('src');
                    imgcache = imgcache.removeWithExpression('path=' + img);
                    $(sender).parent().remove();
                }
            }
        })()
    }
})();

var data = {
    GetDataSource: function (key) {
        switch (key.toLowerCase()) {
            case 'province':
                return data.Province;
        }
    },
    //注意，value必须连续
    Province: [
    { "value": "0", "name": "北京", "ab": "BeiJing", "children": [{ "value": "0", "name": "北京", "ab": "BeiJing"}] },
    { "value": "1", "name": "上海", "ab": "ShangHai", "children": [{ "value": "100", "name": "上海", "ab": "ShangHai"}] },
    { "value": "2", "name": "天津", "ab": "TianJin", "children": [{ "value": "200", "name": "天津", "ab": "TianJin"}] },
    { "value": "3", "name": "重庆", "ab": "ChongQing", "children": [{ "value": "300", "name": "重庆", "ab": "ChongQing"}] },
    { "value": "4", "name": "广东", "ab": "GuangDong", "children": [{ "value": "400", "name": "广州", "ab": "GuangZhou" }, { "value": "401", "name": "深圳", "ab": "ShenZhen" }, { "value": "402", "name": "东莞", "ab": "DongZuo" }, { "value": "403", "name": "珠海", "ab": "ZhuHai" }, { "value": "404", "name": "汕头", "ab": "ShanTou" }, { "value": "405", "name": "佛山", "ab": "FoShan" }, { "value": "406", "name": "江门", "ab": "JiangMen" }, { "value": "407", "name": "中山", "ab": "ZhongShan" }, { "value": "408", "name": "惠州", "ab": "HuiZhou" }, { "value": "409", "name": "茂名", "ab": "MaoMing" }, { "value": "410", "name": "韶关", "ab": "ShaoGuan" }, { "value": "411", "name": "湛江", "ab": "ZhanJiang" }, { "value": "412", "name": "肇庆", "ab": "ZhaoQing" }, { "value": "413", "name": "梅州", "ab": "MeiZhou" }, { "value": "414", "name": "汕尾", "ab": "ShanWei" }, { "value": "415", "name": "河源", "ab": "HeYuan" }, { "value": "416", "name": "阳江", "ab": "YangJiang" }, { "value": "417", "name": "清远", "ab": "QingYuan" }, { "value": "418", "name": "潮州", "ab": "ChaoZhou" }, { "value": "419", "name": "揭阳", "ab": "JieYang" }, { "value": "420", "name": "云浮", "ab": "YunFu"}] },
    { "value": "5", "name": "四川", "ab": "SiChuan", "children": [{ "value": "500", "name": "成都", "ab": "ChengDu" }, { "value": "501", "name": "自贡", "ab": "ZiGong" }, { "value": "502", "name": "泸州", "ab": "ZuoZhou" }, { "value": "503", "name": "德阳", "ab": "DeYang" }, { "value": "504", "name": "绵阳", "ab": "MianYang" }, { "value": "505", "name": "南充", "ab": "NanChong" }, { "value": "506", "name": "凉山", "ab": "LiangShan" }, { "value": "507", "name": "乐山", "ab": "LeShan" }, { "value": "508", "name": "达州", "ab": "DaZhou" }, { "value": "509", "name": "宜宾", "ab": "YiBin" }, { "value": "510", "name": "攀枝花", "ab": "PanZhiHua" }, { "value": "511", "name": "广元", "ab": "GuangYuan" }, { "value": "512", "name": "遂宁", "ab": "SuiNing" }, { "value": "513", "name": "内江", "ab": "NeiJiang" }, { "value": "514", "name": "广安", "ab": "GuangAn" }, { "value": "515", "name": "眉山", "ab": "MeiShan" }, { "value": "516", "name": "雅安", "ab": "YaAn" }, { "value": "517", "name": "巴中", "ab": "BaZhong" }, { "value": "518", "name": "资阳", "ab": "ZiYang" }, { "value": "519", "name": "阿坝", "ab": "ABa" }, { "value": "520", "name": "甘孜", "ab": "GanZi"}] },
    { "value": "6", "name": "浙江", "ab": "ZheJiang", "children": [{ "value": "600", "name": "杭州", "ab": "HangZhou" }, { "value": "601", "name": "宁波", "ab": "NingBo" }, { "value": "602", "name": "温州", "ab": "WenZhou" }, { "value": "603", "name": "嘉兴", "ab": "JiaXing" }, { "value": "604", "name": "湖州", "ab": "HuZhou" }, { "value": "605", "name": "绍兴", "ab": "ShaoXing" }, { "value": "606", "name": "金华", "ab": "JinHua" }, { "value": "607", "name": "衢州", "ab": "ZuoZhou" }, { "value": "608", "name": "舟山", "ab": "ZhouShan" }, { "value": "609", "name": "台州", "ab": "TaiZhou" }, { "value": "610", "name": "丽水", "ab": "LiShui"}] },
    { "value": "7", "name": "贵州", "ab": "GuiZhou", "children": [{ "value": "700", "name": "贵阳", "ab": "GuiYang" }, { "value": "701", "name": "六盘水", "ab": "LiuPanShui" }, { "value": "702", "name": "遵义", "ab": "ZunYi" }, { "value": "703", "name": "安顺", "ab": "AnShun" }, { "value": "704", "name": "铜仁", "ab": "TongRen" }, { "value": "705", "name": "毕节", "ab": "BiJie" }, { "value": "706", "name": "黔西南", "ab": "QianXiNan" }, { "value": "707", "name": "黔东南", "ab": "QianDongNan" }, { "value": "708", "name": "黔南", "ab": "QianNan"}] },
    { "value": "8", "name": "辽宁", "ab": "LiaoNing", "children": [{ "value": "800", "name": "沈阳", "ab": "ShenYang" }, { "value": "801", "name": "大连", "ab": "DaLian" }, { "value": "802", "name": "鞍山", "ab": "AnShan" }, { "value": "803", "name": "抚顺", "ab": "FuShun" }, { "value": "804", "name": "丹东", "ab": "DanDong" }, { "value": "805", "name": "锦州", "ab": "JinZhou" }, { "value": "806", "name": "营口", "ab": "YingKou" }, { "value": "807", "name": "辽阳", "ab": "LiaoYang" }, { "value": "808", "name": "盘锦", "ab": "PanJin" }, { "value": "809", "name": "葫芦岛", "ab": "HuLuDao" }, { "value": "810", "name": "本溪", "ab": "BenXi" }, { "value": "811", "name": "阜新", "ab": "FuXin" }, { "value": "812", "name": "铁岭", "ab": "TieLing" }, { "value": "813", "name": "朝阳", "ab": "ChaoYang"}] },
    { "value": "9", "name": "江苏", "ab": "JiangSu", "children": [{ "value": "900", "name": "南京", "ab": "NanJing" }, { "value": "901", "name": "苏州", "ab": "SuZhou" }, { "value": "902", "name": "无锡", "ab": "WuXi" }, { "value": "903", "name": "徐州", "ab": "XuZhou" }, { "value": "904", "name": "常州", "ab": "ChangZhou" }, { "value": "905", "name": "南通", "ab": "NanTong" }, { "value": "906", "name": "连云港", "ab": "LianYunGang" }, { "value": "907", "name": "淮安", "ab": "HuaiAn" }, { "value": "908", "name": "盐城", "ab": "YanCheng" }, { "value": "909", "name": "扬州", "ab": "YangZhou" }, { "value": "910", "name": "镇江", "ab": "ZhenJiang" }, { "value": "911", "name": "泰州", "ab": "TaiZhou" }, { "value": "912", "name": "宿迁", "ab": "SuQian"}] },
    { "value": "10", "name": "福建", "ab": "FuJian", "children": [{ "value": "1000", "name": "福州", "ab": "FuZhou" }, { "value": "1001", "name": "厦门", "ab": "XiaMen" }, { "value": "1002", "name": "莆田", "ab": "PuTian" }, { "value": "1003", "name": "三明", "ab": "SanMing" }, { "value": "1004", "name": "泉州", "ab": "QuanZhou" }, { "value": "1005", "name": "漳州", "ab": "ZhangZhou" }, { "value": "1006", "name": "南平", "ab": "NanPing" }, { "value": "1007", "name": "龙岩", "ab": "LongYan" }, { "value": "1008", "name": "宁德", "ab": "NingDe"}] },
    { "value": "11", "name": "河北", "ab": "HeBei", "children": [{ "value": "1100", "name": "石家庄", "ab": "ShiJiaZhuang" }, { "value": "1101", "name": "唐山", "ab": "TangShan" }, { "value": "1102", "name": "邯郸", "ab": "HanDan" }, { "value": "1103", "name": "邢台", "ab": "XingTai" }, { "value": "1104", "name": "保定", "ab": "BaoDing" }, { "value": "1105", "name": "张家口", "ab": "ZhangJiaKou" }, { "value": "1106", "name": "承德", "ab": "ChengDe" }, { "value": "1107", "name": "沧州", "ab": "CangZhou" }, { "value": "1108", "name": "廊坊", "ab": "LangFang" }, { "value": "1109", "name": "秦皇岛", "ab": "QinHuangDao" }, { "value": "1110", "name": "衡水", "ab": "HengShui"}] },
    { "value": "12", "name": "河南", "ab": "HeNan", "children": [{ "value": "1200", "name": "郑州", "ab": "ZhengZhou" }, { "value": "1201", "name": "洛阳", "ab": "LuoYang" }, { "value": "1202", "name": "平顶山", "ab": "PingDingShan" }, { "value": "1203", "name": "焦作", "ab": "JiaoZuo" }, { "value": "1204", "name": "鹤壁", "ab": "HeBi" }, { "value": "1205", "name": "新乡", "ab": "XinXiang" }, { "value": "1206", "name": "安阳", "ab": "AnYang" }, { "value": "1207", "name": "南阳", "ab": "NanYang" }, { "value": "1208", "name": "漯河", "ab": "ZuoHe" }, { "value": "1209", "name": "济源", "ab": "JiYuan" }, { "value": "1210", "name": "开封", "ab": "KaiFeng" }, { "value": "1211", "name": "濮阳", "ab": "ZuoYang" }, { "value": "1212", "name": "许昌", "ab": "XuChang" }, { "value": "1213", "name": "三门峡", "ab": "SanMenXia" }, { "value": "1214", "name": "商丘", "ab": "ShangQiu" }, { "value": "1215", "name": "信阳", "ab": "XinYang" }, { "value": "1216", "name": "周口", "ab": "ZhouKou" }, { "value": "1217", "name": "驻马店", "ab": "ZhuMaDian"}] },
    { "value": "13", "name": "吉林", "ab": "JiLin", "children": [{ "value": "1300", "name": "长春", "ab": "ChangChun" }, { "value": "1301", "name": "吉林", "ab": "JiLin" }, { "value": "1302", "name": "四平", "ab": "SiPing" }, { "value": "1303", "name": "辽源", "ab": "LiaoYuan" }, { "value": "1304", "name": "通化", "ab": "TongHua" }, { "value": "1305", "name": "白山", "ab": "BaiShan" }, { "value": "1306", "name": "松原", "ab": "SongYuan" }, { "value": "1307", "name": "白城", "ab": "BaiCheng" }, { "value": "1308", "name": "延边", "ab": "YanBian"}] },
    { "value": "14", "name": "黑龙江", "ab": "HeiLongJiang", "children": [{ "value": "1400", "name": "哈尔滨", "ab": "HaErBin" }, { "value": "1401", "name": "齐齐哈尔", "ab": "QiQiHaEr" }, { "value": "1402", "name": "鸡西", "ab": "JiXi" }, { "value": "1403", "name": "鹤岗", "ab": "HeGang" }, { "value": "1404", "name": "双鸭山", "ab": "ShuangYaShan" }, { "value": "1405", "name": "大庆", "ab": "DaQing" }, { "value": "1406", "name": "伊春", "ab": "YiChun" }, { "value": "1407", "name": "佳木斯", "ab": "JiaMuSi" }, { "value": "1408", "name": "黑河", "ab": "HeiHe" }, { "value": "1409", "name": "绥化", "ab": "SuiHua" }, { "value": "1410", "name": "七台河", "ab": "QiTaiHe" }, { "value": "1411", "name": "牡丹江", "ab": "MuDanJiang" }, { "value": "1412", "name": "大兴安岭", "ab": "DaXingAnLing"}] },
    { "value": "15", "name": "山东", "ab": "ShanDong", "children": [{ "value": "1500", "name": "济南", "ab": "JiNan" }, { "value": "1501", "name": "青岛", "ab": "QingDao" }, { "value": "1502", "name": "威海", "ab": "WeiHai" }, { "value": "1503", "name": "淄博", "ab": "ZiBo" }, { "value": "1504", "name": "枣庄", "ab": "ZaoZhuang" }, { "value": "1505", "name": "东营", "ab": "DongYing" }, { "value": "1506", "name": "烟台", "ab": "YanTai" }, { "value": "1507", "name": "潍坊", "ab": "WeiFang" }, { "value": "1508", "name": "莱芜", "ab": "LaiWu" }, { "value": "1509", "name": "滨州", "ab": "BinZhou" }, { "value": "1510", "name": "济宁", "ab": "JiNing" }, { "value": "1511", "name": "泰安", "ab": "TaiAn" }, { "value": "1512", "name": "日照", "ab": "RiZhao" }, { "value": "1513", "name": "临沂", "ab": "LinYi" }, { "value": "1514", "name": "德州", "ab": "DeZhou" }, { "value": "1515", "name": "聊城", "ab": "LiaoCheng" }, { "value": "1516", "name": "菏泽", "ab": "HeZe"}] },
    { "value": "16", "name": "安徽", "ab": "AnHui", "children": [{ "value": "1600", "name": "合肥", "ab": "HeFei" }, { "value": "1601", "name": "芜湖", "ab": "WuHu" }, { "value": "1602", "name": "蚌埠", "ab": "BangBu" }, { "value": "1603", "name": "马鞍山", "ab": "MaAnShan" }, { "value": "1604", "name": "安庆", "ab": "AnQing" }, { "value": "1605", "name": "滁州", "ab": "ChuZhou" }, { "value": "1606", "name": "阜阳", "ab": "FuYang" }, { "value": "1607", "name": "宿州", "ab": "SuZhou" }, { "value": "1608", "name": "巢湖", "ab": "ChaoHu" }, { "value": "1609", "name": "六安", "ab": "LiuAn" }, { "value": "1610", "name": "淮南", "ab": "HuaiNan" }, { "value": "1611", "name": "淮北", "ab": "HuaiBei" }, { "value": "1612", "name": "铜陵", "ab": "TongLing" }, { "value": "1613", "name": "黄山", "ab": "HuangShan" }, { "value": "1614", "name": "亳州", "ab": "ZuoZhou" }, { "value": "1615", "name": "池州", "ab": "ChiZhou" }, { "value": "1616", "name": "宣城", "ab": "XuanCheng"}] },
    { "value": "17", "name": "广西", "ab": "GuangXi", "children": [{ "value": "1700", "name": "南宁", "ab": "NanNing" }, { "value": "1701", "name": "桂林", "ab": "GuiLin" }, { "value": "1702", "name": "柳州", "ab": "LiuZhou" }, { "value": "1703", "name": "梧州", "ab": "WuZhou" }, { "value": "1704", "name": "钦州", "ab": "QinZhou" }, { "value": "1705", "name": "贵港", "ab": "GuiGang" }, { "value": "1706", "name": "玉林", "ab": "YuLin" }, { "value": "1707", "name": "百色", "ab": "BaiSe" }, { "value": "1708", "name": "河池", "ab": "HeChi" }, { "value": "1709", "name": "来宾", "ab": "LaiBin" }, { "value": "1710", "name": "北海", "ab": "BeiHai" }, { "value": "1711", "name": "防城港", "ab": "FangChengGang" }, { "value": "1712", "name": "贺州", "ab": "HeZhou" }, { "value": "1713", "name": "崇左", "ab": "ChongZuo"}] },
    { "value": "18", "name": "海南", "ab": "HaiNan", "children": [{ "value": "1800", "name": "海口", "ab": "HaiKou" }, { "value": "1801", "name": "三亚", "ab": "SanYa"}] },
    { "value": "19", "name": "内蒙古", "ab": "NeiMengGu", "children": [{ "value": "1900", "name": "呼和浩特", "ab": "HuHeHaoTe" }, { "value": "1901", "name": "包头", "ab": "BaoTou" }, { "value": "1902", "name": "乌海", "ab": "WuHai" }, { "value": "1903", "name": "赤峰", "ab": "ChiFeng" }, { "value": "1904", "name": "通辽", "ab": "TongLiao" }, { "value": "1905", "name": "鄂尔多斯", "ab": "EErDuoSi" }, { "value": "1906", "name": "呼伦贝尔", "ab": "HuLunBeiEr" }, { "value": "1907", "name": "巴彦淖尔", "ab": "BaYanNaoEr" }, { "value": "1908", "name": "乌兰察布", "ab": "WuLanChaBu" }, { "value": "1909", "name": "兴安", "ab": "XingAn" }, { "value": "1910", "name": "锡林郭勒", "ab": "XiLinGuoLe" }, { "value": "1911", "name": "阿拉善", "ab": "ALaShan"}] },
    { "value": "20", "name": "山西", "ab": "ShanXi", "children": [{ "value": "2000", "name": "太原", "ab": "TaiYuan" }, { "value": "2001", "name": "大同", "ab": "DaTong" }, { "value": "2002", "name": "阳泉", "ab": "YangQuan" }, { "value": "2003", "name": "长治", "ab": "ChangZhi" }, { "value": "2004", "name": "晋城", "ab": "JinCheng" }, { "value": "2005", "name": "朔州", "ab": "ShuoZhou" }, { "value": "2006", "name": "晋中", "ab": "JinZhong" }, { "value": "2007", "name": "运城", "ab": "YunCheng" }, { "value": "2008", "name": "忻州", "ab": "XinZhou" }, { "value": "2009", "name": "临汾", "ab": "LinFen" }, { "value": "2010", "name": "吕梁", "ab": "LvLiang"}] },
    { "value": "21", "name": "宁夏", "ab": "NingXia", "children": [{ "value": "2100", "name": "银川", "ab": "YinChuan" }, { "value": "2101", "name": "石嘴山", "ab": "ShiZuiShan" }, { "value": "2102", "name": "吴忠", "ab": "WuZhong" }, { "value": "2103", "name": "固原", "ab": "GuYuan" }, { "value": "2104", "name": "中卫", "ab": "ZhongWei"}] },
    { "value": "22", "name": "甘肃", "ab": "GanSu", "children": [{ "value": "2200", "name": "兰州", "ab": "LanZhou" }, { "value": "2201", "name": "金昌", "ab": "JinChang" }, { "value": "2202", "name": "白银", "ab": "BaiYin" }, { "value": "2203", "name": "天水", "ab": "TianShui" }, { "value": "2204", "name": "武威", "ab": "WuWei" }, { "value": "2205", "name": "张掖", "ab": "ZhangYe" }, { "value": "2206", "name": "平凉", "ab": "PingLiang" }, { "value": "2207", "name": "酒泉", "ab": "JiuQuan" }, { "value": "2208", "name": "庆阳", "ab": "QingYang" }, { "value": "2209", "name": "定西", "ab": "DingXi" }, { "value": "2210", "name": "嘉峪关", "ab": "JiaYuGuan" }, { "value": "2211", "name": "陇南", "ab": "LongNan" }, { "value": "2212", "name": "临夏", "ab": "LinXia" }, { "value": "2213", "name": "甘南", "ab": "GanNan"}] },
    { "value": "23", "name": "陕西", "ab": "ShanXi", "children": [{ "value": "2300", "name": "西安", "ab": "XiAn" }, { "value": "2301", "name": "铜川", "ab": "TongChuan" }, { "value": "2302", "name": "宝鸡", "ab": "BaoJi" }, { "value": "2303", "name": "咸阳", "ab": "XianYang" }, { "value": "2304", "name": "渭南", "ab": "WeiNan" }, { "value": "2305", "name": "延安", "ab": "YanAn" }, { "value": "2306", "name": "汉中", "ab": "HanZhong" }, { "value": "2307", "name": "榆林", "ab": "YuLin" }, { "value": "2308", "name": "安康", "ab": "AnKang" }, { "value": "2309", "name": "商洛", "ab": "ShangLuo"}] },
    { "value": "24", "name": "青海", "ab": "QingHai", "children": [{ "value": "2400", "name": "西宁", "ab": "XiNing" }, { "value": "2401", "name": "海东", "ab": "HaiDong" }, { "value": "2402", "name": "海北", "ab": "HaiBei" }, { "value": "2403", "name": "黄南", "ab": "HuangNan" }, { "value": "2404", "name": "海南州", "ab": "HaiNanZhou" }, { "value": "2405", "name": "果洛", "ab": "GuoLuo" }, { "value": "2406", "name": "玉树", "ab": "YuShu" }, { "value": "2407", "name": "海西", "ab": "HaiXi"}] },
    { "value": "25", "name": "湖北", "ab": "HuBei", "children": [{ "value": "2500", "name": "武汉", "ab": "WuHan" }, { "value": "2501", "name": "黄石", "ab": "HuangShi" }, { "value": "2502", "name": "襄阳", "ab": "XiangYang" }, { "value": "2503", "name": "十堰", "ab": "ShiYan" }, { "value": "2504", "name": "荆州", "ab": "JingZhou" }, { "value": "2505", "name": "宜昌", "ab": "YiChang" }, { "value": "2506", "name": "荆门", "ab": "JingMen" }, { "value": "2507", "name": "鄂州", "ab": "EZhou" }, { "value": "2508", "name": "仙桃", "ab": "XianTao" }, { "value": "2509", "name": "潜江", "ab": "QianJiang" }, { "value": "2510", "name": "孝感", "ab": "XiaoGan" }, { "value": "2511", "name": "黄冈", "ab": "HuangGang" }, { "value": "2512", "name": "咸宁", "ab": "XianNing" }, { "value": "2513", "name": "随州", "ab": "SuiZhou" }, { "value": "2514", "name": "恩施", "ab": "EnShi" }, { "value": "2515", "name": "天门　", "ab": "TianMen" }, { "value": "2516", "name": "神农架", "ab": "ShenNongJia"}] },
    { "value": "26", "name": "湖南", "ab": "HuNan", "children": [{ "value": "2600", "name": "长沙", "ab": "ChangSha" }, { "value": "2601", "name": "株洲", "ab": "ZhuZhou" }, { "value": "2602", "name": "湘潭", "ab": "XiangTan" }, { "value": "2603", "name": "衡阳", "ab": "HengYang" }, { "value": "2604", "name": "邵阳", "ab": "ShaoYang" }, { "value": "2605", "name": "岳阳", "ab": "YueYang" }, { "value": "2606", "name": "常德", "ab": "ChangDe" }, { "value": "2607", "name": "郴州", "ab": "ChenZhou" }, { "value": "2608", "name": "永州", "ab": "YongZhou" }, { "value": "2609", "name": "娄底", "ab": "LouDi" }, { "value": "2610", "name": "张家界", "ab": "ZhangJiaJie" }, { "value": "2611", "name": "益阳", "ab": "YiYang" }, { "value": "2612", "name": "怀化", "ab": "HuaiHua" }, { "value": "2613", "name": "湘西", "ab": "XiangXi"}] },
    { "value": "27", "name": "江西", "ab": "JiangXi", "children": [{ "value": "2700", "name": "南昌", "ab": "NanChang" }, { "value": "2701", "name": "景德镇", "ab": "JingDeZhen" }, { "value": "2702", "name": "萍乡", "ab": "PingXiang" }, { "value": "2703", "name": "九江", "ab": "JiuJiang" }, { "value": "2704", "name": "新余", "ab": "XinYu" }, { "value": "2705", "name": "鹰潭", "ab": "YingTan" }, { "value": "2706", "name": "赣州", "ab": "GanZhou" }, { "value": "2707", "name": "上饶", "ab": "ShangRao" }, { "value": "2708", "name": "吉安", "ab": "JiAn" }, { "value": "2709", "name": "抚州", "ab": "FuZhou" }, { "value": "2710", "name": "宜春", "ab": "YiChun"}] },
    { "value": "28", "name": "云南", "ab": "YunNan", "children": [{ "value": "2800", "name": "昆明", "ab": "KunMing" }, { "value": "2801", "name": "曲靖", "ab": "QuJing" }, { "value": "2802", "name": "玉溪", "ab": "YuXi" }, { "value": "2803", "name": "保山", "ab": "BaoShan" }, { "value": "2804", "name": "昭通", "ab": "ZhaoTong" }, { "value": "2805", "name": "红河", "ab": "HongHe" }, { "value": "2806", "name": "西双版纳", "ab": "XiShuangBanNa" }, { "value": "2807", "name": "楚雄", "ab": "ChuXiong" }, { "value": "2808", "name": "大理", "ab": "DaLi" }, { "value": "2809", "name": "德宏", "ab": "DeHong" }, { "value": "2810", "name": "丽江", "ab": "LiJiang" }, { "value": "2811", "name": "普洱", "ab": "PuEr" }, { "value": "2812", "name": "临沧", "ab": "LinCang" }, { "value": "2813", "name": "文山", "ab": "WenShan" }, { "value": "2814", "name": "怒江", "ab": "NuJiang" }, { "value": "2815", "name": "迪庆", "ab": "DiQing"}] },
    { "value": "29", "name": "新疆", "ab": "XinJiang", "children": [{ "value": "2900", "name": "乌鲁木齐", "ab": "WuLuMuQi" }, { "value": "2901", "name": "克拉玛依", "ab": "KeLaMaYi" }, { "value": "2902", "name": "吐鲁番", "ab": "TuLuFan" }, { "value": "2903", "name": "哈密", "ab": "HaMi" }, { "value": "2904", "name": "和田", "ab": "HeTian" }, { "value": "2905", "name": "阿克苏", "ab": "AKeSu" }, { "value": "2906", "name": "喀什", "ab": "KaShi" }, { "value": "2907", "name": "伊犁", "ab": "YiLi" }, { "value": "2908", "name": "石河子", "ab": "ShiHeZi" }, { "value": "2909", "name": "巴音郭楞", "ab": "BaYinGuoLeng" }, { "value": "2910", "name": "克孜勒苏", "ab": "KeZiLeSu" }, { "value": "2911", "name": "昌吉", "ab": "ChangJi" }, { "value": "2912", "name": "博尔塔拉", "ab": "BoErTaLa" }, { "value": "2913", "name": "塔城", "ab": "TaCheng" }, { "value": "2914", "name": "阿勒泰", "ab": "ALeTai"}] },
    { "value": "30", "name": "西藏", "ab": "XiCang", "children": [{ "value": "3000", "name": "拉萨", "ab": "LaSa" }, { "value": "3001", "name": "昌都", "ab": "ChangDu" }, { "value": "3002", "name": "山南", "ab": "ShanNan" }, { "value": "3003", "name": "日喀则", "ab": "RiKaZe" }, { "value": "3004", "name": "那曲", "ab": "NaQu" }, { "value": "3005", "name": "阿里", "ab": "ALi" }, { "value": "3006", "name": "林芝", "ab": "LinZhi"}]}]
}

Array.prototype.contain = function (val) {
    var arr = this;
    for (var i = 0, len = arr.length; i < len; i++) {
        if (arr[i] == val) {
            return true;
        }
    }
    return false;
}
//Array.prototype.remove = function (expression) {
//    if (expression) {
//        var piece = expression.split('=');
//        if (piece.length == 2) {
//            var left = piece[0];
//            var right = piece[1];
//            var newarr = [];
//            for (var i = 0, len = this.length; i < len; i++) {
//                if (eval('this[i].' + left).trim() != right.trim()) {
//                    newarr.push(this[i]);
//                }
//            }
//            return newarr;
//        }
//    }
//    return this;
//}

Array.prototype.remove = function (val) {
    if (val) {
        var newarr = [];
        for (var i = 0, len = this.length; i < len; i++) {
            if (this[i].trim() != val.trim()) {
                newarr.push(this[i]);
            }
        }
        return newarr;
    }
}

Array.prototype.removeWithExpression = function (expression) {
    if (expression) {
        var piece = expression.split('=');
        if (piece.length == 2) {
            var left = piece[0];
            var right = piece[1];
            var newarr = [];
            for (var i = 0, len = this.length; i < len; i++) {
                if (eval('this[i].' + left).trim() != right.trim()) {
                    newarr.push(this[i]);
                }
            }
            return newarr;
        }
    }
    return this;
}

String.prototype.trim = function () {
    return this.toString().replace(/^\s*|\s*$/g, '');
}

var dateHelper = (function () {
    Replace = function (str, from, to) {
        return str.split(from).join(to);
    };
    GetFullMonth = function (date) {
        var v = date.getMonth() + 1;
        if (v > 9) return v.toString();
        return "0" + v;
    };
    GetFullDate = function (date) {
        var v = date.getDate();
        if (v > 9) return v.toString();
        return "0" + v;
    };
    GetHarfYear = function (date) {
        var v = date.getYear();
        if (v > 9) return v.toString();
        return "0" + v;
    };
    return function () {
        this.AddDays = function (date, value) {
            return new Date(date.setDate(date.getDate() + value));
        },
        this.AddMonths = function (date, value) {
            return new Date(date.setMonth(date.getMonth() + value));
        },
        this.AddYears = function (date, value) {
            return new Date(date.setFullYear(date.getFullYear() + value));
        },
        this.Format = function (date, pattern) {
            pattern = Replace(pattern, "yyyy", date.getFullYear());
            pattern = Replace(pattern, "MM", GetFullMonth(date));
            pattern = Replace(pattern, "dd", GetFullDate(date));
            pattern = Replace(pattern, "yy", GetHarfYear(date));
            pattern = Replace(pattern, "M", date.getMonth() + 1);
            pattern = Replace(pattern, "d", date.getDate());
            pattern = Replace(pattern, "hh", date.getHours());
            pattern = Replace(pattern, "mm", date.getMinutes());
            pattern = Replace(pattern, "ss", date.getSeconds());
            return pattern;
        }
    }
})();
