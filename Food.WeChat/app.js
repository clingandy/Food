//app.js
const common = require('/utils/common.js')
App({
  onLaunch: function () {

    wx.showShareMenu({
      withShareTicket: true
    })
    // 展示本地存储能力
    //var logs = wx.getStorageSync('logs') || []
    //logs.unshift(Date.now())
    //wx.setStorageSync('logs', logs)

    // 登录
    wx.login({
      success: res => {
        // 发送 res.code 到后台换取 openId, sessionKey, unionId
        var _this=this;
        common.getData({
          url: "/WeChat/GetJscode2session",
          isNoFail: true,
          isNoLoading: true,
          params: {
            code: res.code,
          },
          success: function (res, s, m) {
            _this.globalData.openId = res.openid;//获取到的openid  
            _this.globalData.sessionKey = res.session_key;//获取到session_key  
            _this.postPhoneInfo();
          },
          fail: function () {
          }
        })
      }
    })
    
    // 获取用户信息
    wx.getSetting({
      success: res => {
        if (res.authSetting['scope.userInfo']) {
          // 已经授权，可以直接调用 getUserInfo 获取头像昵称，不会弹框
          wx.getUserInfo({
            success: res => {
              // 可以将 res 发送给后台解码出 unionId
              this.globalData.userInfo = res.userInfo;
              setTimeout(this.postMemberInfo, 1000);
              // 由于 getUserInfo 是网络请求，可能会在 Page.onLoad 之后才返回
              // 所以此处加入 callback 以防止这种情况
              if (this.userInfoReadyCallback) {
                this.userInfoReadyCallback(res)
              }
            }
          })
        }
      }
    })
  },

  postMemberInfo:function(){
    // 调用接口存储用户数据
    var _this =this;
    if (!_this.globalData.userInfo || !_this.globalData.openId) {
      return;
    }
    common.postData({
      url: "/rfmember/addRfMember",
      isNoFail: true,
      isNoLoading: true,
      params: {
        openId: _this.globalData.openId,
        nickName: _this.globalData.userInfo.nickName,
        avatarUrl: _this.globalData.userInfo.avatarUrl,
        gender: _this.globalData.userInfo.gender,
        country: _this.globalData.userInfo.country,
        province: _this.globalData.userInfo.province,
        city: _this.globalData.userInfo.city
      },
      success: function (res, s, m) {

      },
      fail: function () {

      }
    })
  },
  postPhoneInfo: function () {
    // 调用接口存储用户数据
    var _this = this;
    if (!_this.globalData.openId) {
      return;
    }
    wx.getSystemInfo({
      success: res => {
        common.postData({
          url: "/RfPhoneDevice/AddRfPhoneDevice",
          isNoFail: true,
          isNoLoading: true,
          params: {
            openId : _this.globalData.openId,
            model : res.model,
            brand: res.brand,
            pixelRatio : res.pixelRatio,
            screenWidth: res.screenWidth,
            screenHeight: res.screenHeight,
            language : res.language,
            version : res.version,
            platform : res.platform,
            system: res.system,
            benchmarkLevel: res.benchmarkLevel
          },
          success: function (res, s, m) {

          },
          fail: function () {

          }
        })
      }
    });
  },

  globalData: {
    userInfo: null,
    mapKey: common.mapKey,
    openId:'',
  }
})