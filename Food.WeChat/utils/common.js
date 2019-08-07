// const apiUrl = "https://localhost:44335/api";
const apiUrl = "https://www.feelfreeeat.top/api";
const imgUrl = "https://www.feelfreeeat.top";
const mapKey = 'ZM2BZ-7JU3V-2WQP3-UEUO3-MBAHF-QXF2U';

const pageSize = 10;

//http请求封装
const requestHandlerBase = {
  method: "GET",
  url: "",
  filePath: "",    //上传才使用的参数
  isNoLoading: false,
  isNoFail: false,
  params: {},
  success: function (res, status, errMsg) {
    // success 
  },
  fail: function () {
    // fail
  },
}
//GET请求  
function getData(requestHandler) {
  requestHandler = Object.assign({}, requestHandlerBase, requestHandler)
  request(requestHandler)
}
//POST请求  
function postData(requestHandler) {
  requestHandler = Object.assign({}, requestHandlerBase, requestHandler)
  if (requestHandler.method == 'GET'){
    requestHandler.method = 'POST'
  }
  request(requestHandler)
}
//PostUpload请求
function postUpload(requestHandler) {
  uploadFile(requestHandler)
}

//请求
function request(requestHandler) {
  if (!requestHandler.isNoLoading) {
    wx.showLoading({
      title: '加载中',
    })
  }
  //注意：可以对params加密等处理  
  var params = requestHandler.params;
  if (requestHandler.method !== 'GET'){
    params = JSON.stringify(params)
  }
  wx.request({
    url: apiUrl + requestHandler.url,
    data: params,
    method: requestHandler.method,
    header: {
      //'content-type': 'application/x-www-form-urlencoded',
      'content-type': 'application/json',
      //'Cookie': "token=" + getToken()
    },
    dataType: "json",
    success: function (res) {
      wx.hideLoading();
      var msg = getHttpRequestStatus(res.statusCode);
      var status = true;
      if (msg != '') {  //请求错误
        status = false;
        requestHandler.success(res, status, msg);
      } else if (!getUserHttpRequestInfo(res.data.code)) { //业务错误
        status = false;
        requestHandler.success(res, status, res.data.msg);
      } else {  //成功
        requestHandler.success(res.data.data, status, '');
        // if (res.data.data == "" || typeof (res.data.data) == "object"){
        //   requestHandler.success(res.data.data, status, '');
        // }
        // else{
        //   requestHandler.success(JSON.parse(res.data.data), status, '');
        // }
      }
    },
    fail: function () {
      wx.hideLoading();
      if (requestHandler.isNoFail){
        return;
      }
      wx.showModal({
        title: '提示',
        content: '页面发生未知的错误，请刷新重试',
        success: function (res) {
          // if (res.confirm) {
          //   console.log('确定')
          // } else if (res.cancel) {
          //   console.log('取消')
          // }
        }
      });
      requestHandler.fail()
    },
    complete: function () {
      //wx.hideLoading();
      // complete  
    }
  })
}

//上传
function uploadFile(requestHandler) {
  wx.showLoading({
    title: '加载中',
  })
  wx.uploadFile({
    url: apiUrl + requestHandler.url,
    filePath: requestHandler.filePath,
    name: 'file',
    header: {
      "Content-Type": "multipart/form-data",
      'Cookie': getToken()
    },
    formData: requestHandler.params,
    success: function (res) {
      wx.hideLoading();
      var msg = getHttpRequestStatus(res.statusCode);
      var status = true;
      if (msg != '') {  //请求错误
        status = false;
        requestHandler.success(res, status, msg);
      } else if (!getUserHttpRequestInfo(res.data.code)) { //业务错误
        status = false;
        requestHandler.success(res, status, res.data.msg);
      } else {  //成功
        requestHandler.success(res, status, '');
      }
    },
    fail: function () {
      wx.hideLoading();
      wx.showModal({
        title: '提示',
        content: '页面发生未知的错误，请刷新重试',
        success: function (res) {
          // if (res.confirm) {
          //   console.log('确定')
          // } else if (res.cancel) {
          //   console.log('取消')
          // }
        }
      });
      requestHandler.fail()
    },
    complete: function () {
      //wx.hideLoading();
      // complete  
    }
  })
}

//根据http返回状态返回信息
function getHttpRequestStatus(status) {
  var msg = "未知错误"
  switch (status) {
    case 200:
      msg = "";
      break;
    case 400:
      msg = "页面无效";
      break;
    case 401:
      msg = "页面未授权";
      break;
    case 404:
      msg = "页面不存在";
      break;
    case 413:
      msg = "文件过大";
      break;
    case 505:
      msg = "内部错误";
      break;
    case 504:
      msg = "网关超时";
      break;
  }
  return msg;
}

//根据用户设定返回状态返回信息
function getUserHttpRequestInfo(status) {
  var isok = false;
  switch (status) {
    case 200:
      isok = true;
      break;
  }
  return isok;
}

/**
 * getCache
 */
function getCache(key) {
  var value = "";
  try {
    value = wx.getStorageSync(key)
  } catch (e) {
  }
  return value;
}
/**
 * setCache
 */
function setCache(key, obj) {
  wx.setStorageSync(key, obj);
}
/**
 * 去除空格
 */
function trim(str) {
  return str.replace(/(^\s*)|(\s*$)/g, "");
}
/**
 * 时间格式
 */
function getLocalTime(nS) {
  return new Date(parseInt(nS) * 1000).toLocaleString().replace(/:\d{1,2}$/, ' ');
}
/**
 * 提示消息
 */
function warn(msg) {
  setTimeout(function () {
    wx.showToast({
      title: msg,
      image: '/img/warn.png',
      duration: 1500
    })
  }, 500);
}
/**
 * 成功消息
 */
function alert(msg) {
  setTimeout(function () {
    wx.showToast({
      title: msg,
      duration: 1500
    })
  }, 500);
}
/**
 * 失败消息
 */
function alertError(msg) {
  setTimeout(function () {
    wx.showToast({
      title: msg,
      image: '/img/error.png',
      duration: 1500
    })
  }, 500);
}
/**
 * 确认消息
 */
function alertConfirm(title,msg) {
  setTimeout(function () {
    wx.showModal({
      title: title,
      content: msg,
      success: function (res) {
        // if (res.confirm) {
        //   console.log('确定')
        // } else if (res.cancel) {
        //   console.log('取消')
        // }
      }
    });
  }, 500);
}

module.exports = {
  imgUrl: imgUrl,
  apiUrl: apiUrl,
  mapKey: mapKey,
  pageSize: pageSize,
  getData: getData,
  postData: postData,
  postUpload: postUpload,
  getCache: getCache,
  setCache: setCache,
  trim: trim,
  getLocalTime: getLocalTime,
  warn:warn,
  alert: alert,
  alertConfirm: alertConfirm,
  alertError: alertError,
}