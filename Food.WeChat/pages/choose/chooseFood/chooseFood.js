// pages/choose/chooseFood.js
const app = getApp()
var common = require('../../../utils/common.js')
var _key = 'chooseFoodListCache'
Page({

  /**
   * 页面的初始数据
   */
  data: {
    imgUrl: common.imgUrl,
  },

  // 清空
  cleanFoodCache: function () {
    var cache = common.setCache(_key, []);
    this.setData({
      chooseFoodListCache: [],
    })
  },

  // 删除
  delFoodCache: function(e){
    var _this = this;
    var id = e.currentTarget.id;
    var cache = _this.data.chooseFoodListCache;
    if (id <= cache.length && id >= 0) {
      cache.splice(id, 1);
      common.setCache(_key, cache);
      _this.setData({
        chooseFoodListCache: cache,
      })
    }
  },

  // 选择食物
  chooseFood: function () {
    var _this = this;
    if (_this.data.startingChoose || _this.data.chooseFoodListCache.length == 0) {
      return;
    }
    _this.setData({
      startingChoose: true
    });
    var length = _this.data.chooseFoodListCache.length;
    var randomNun = Math.round(Math.random() * length);
    if (randomNun > length - 1) {
      randomNun = 0;
    }
    var food = _this.data.chooseFoodListCache[randomNun];
    wx.showModal({
      title: '选择美食',
      content: food.name,
      success: function (res) {
      },
      complete: function (res) {
        _this.setData({
          startingChoose: false
        });
      }
    });
    _this.saveChooseFood(food)
  },

  // 保存选择店铺数据
  saveChooseFood: function (food) {
    common.postData({
      url: "/RfChooseFood/AddRfChooseFood",
      isNoFail: true,
      isNoLoading: true,
      params: {
        openId: app.globalData.openId,
        foodId: food.id,
        foodName: food.name
      },
      success: function (res, s, m) {
      },
      fail: function () {
      }
    })
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    var cache = common.getCache(_key);
    if (!cache) {
      cache = [];
    }
    this.setData({
      chooseFoodListCache: cache,
    })
  },

  /**
   * 生命周期函数--监听页面初次渲染完成
   */
  onReady: function () {

  },

  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function () {

  },

  /**
   * 生命周期函数--监听页面隐藏
   */
  onHide: function () {

  },

  /**
   * 生命周期函数--监听页面卸载
   */
  onUnload: function () {

  },

  /**
   * 页面相关事件处理函数--监听用户下拉动作
   */
  onPullDownRefresh: function () {

  },

  /**
   * 页面上拉触底事件的处理函数
   */
  onReachBottom: function () {

  },

  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function () {

  }
})