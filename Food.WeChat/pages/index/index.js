//index.js
const app = getApp()
const common = require('../../utils/common.js')
Page({
  data: {
    motto: 'Hello World',
    userInfo: {},
    hasUserInfo: false,
    canIUse: wx.canIUse('button.open-type.getUserInfo'),
    visible: false
  },
  //事件处理函数
  show: function () {
    this.getShareQRCode();
    this.setData({ visible: true })
  },
  close: function () {
    this.setData({ visible: false })
  },
  upper: function (e) {
    //console.log(e)
  },
  lower: function (e) {
    //console.log(e)
  },
  scroll: function (e) {
    //console.log(e)
  },
  //事件处理函数
  bindViewTap: function() {
    wx.navigateTo({
      //url: '../logs/logs'
      url: '../map/map'
    })
  },
  onLoad: function () {
    if (app.globalData.userInfo) {
      this.setData({
        userInfo: app.globalData.userInfo,
        hasUserInfo: true
      });
    } else if (this.data.canIUse){
      // 由于 getUserInfo 是网络请求，可能会在 Page.onLoad 之后才返回
      // 所以此处加入 callback 以防止这种情况
      app.userInfoReadyCallback = res => {
        this.setData({
          userInfo: res.userInfo,
          hasUserInfo: true
        })
      }
    } else {
      // 在没有 open-type=getUserInfo 版本的兼容处理
      wx.getUserInfo({
        success: res => {
          app.globalData.userInfo = res.userInfo
          this.setData({
            userInfo: res.userInfo,
            hasUserInfo: true
          });
        }
      })
    }
  },
  getUserInfo: function(e) {
    app.globalData.userInfo = e.detail.userInfo
    this.setData({
      userInfo: e.detail.userInfo,
      hasUserInfo: true
    })
  },

  // 分享二维码
  getShareQRCode: function () {
    let _this = this;
    common.getData({
      url: "/WeChat/GetCreatewxaqrcode",
      params: {
        url: 'pages/index/index',
      },
      success: function (res, s, m) {
        if(s){
          _this.setData({
            imgUrl: res,
          });
        }
      },
      fail: function () {
      }
    })
  }

})

