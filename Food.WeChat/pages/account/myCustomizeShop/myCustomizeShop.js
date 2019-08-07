// pages/account/myFood/myFood.js
const app = getApp()
var common = require('../../../utils/common.js')
Page({

  /**
   * 页面的初始数据
   */
  data: {
    shopList: [],
    lableIndex: 7,
    lable: 8,
    lableArray: [
      { id: 1, name: '春' },
      { id: 2, name: '夏' },
      { id: 3, name: '秋' },
      { id: 4, name: '冬' },
      { id: 5, name: '住处' },
      { id: 6, name: '公司' },
      { id: 7, name: '老家' },
      { id: 8, name: '临时' }
    ],
  },

  // 删除
  delShopHandler: function (e) {
    var _this = this;
    var id = e.currentTarget.id;
    var shopList = _this.data.shopList;
    if (id <= shopList.length && id >= 0) {
      wx.showModal({
        title: '提示',
        content: '确定删除当前收藏',
        success: function (res) {
          if (res.confirm) {
            var delShop = shopList.splice(id, 1);
            common.postData({
              method: "DELETE",
              url: "/RfMemberCustomizeShop/DelRfMemberCustomizeShopById?id=" + delShop[0].id,
              params: {},
              success: function (res, s, m) {
                _this.setData({
                  shopList: shopList,
                })
              },
              fail: function () { }
            })
           
          } else if (res.cancel) {
            console.log('取消')
          }
        }
      });
    }
  },

  // 选择食物
  chooseShop: function () {
    var _this = this;
    if (_this.data.startingChoose || _this.data.shopList.length == 0) {
      return;
    }
    _this.setData({
      startingChoose: true
    });
    var length = _this.data.shopList.length;
    var randomNun = Math.round(Math.random() * length);
    if (randomNun > length - 1) {
      randomNun = 0;
    }
    var shop = _this.data.shopList[randomNun];
    wx.showModal({
      title: '选择收藏',
      content: shop.title,
      success: function (res) {
      },
      complete: function (res) {
        _this.setData({
          startingChoose: false
        });
      }
    });
    _this.saveChooseShop(shop)
  },

  // 保存选择店铺数据
  saveChooseShop: function (shop) {
    common.postData({
      url: "/RfChooseCustomizeShop/AddRfChooseCustomizeShop",
      isNoFail: true,
      isNoLoading: true,
      params: {
        openId: app.globalData.openId,
        shopId: shop.id,
        shopTitle: shop.title,
        category: shop.category,
        lable: shop.lable
      },
      success: function (res, s, m) {

      },
      fail: function () {

      }
    })
  },

  // 标签选择
  bindLableChange: function(e){
    var lable = this.data.lableArray[e.detail.value].id;
    this.setData({
      lableIndex: e.detail.value,
      lable: lable
    })
    searchList(this);
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    searchList(this);
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

function searchList(_this) {
  common.getData({
    url: "/RfMemberCustomizeShop/GetRfMemberCustomizeShopPageList",
    params: {
      // pageIndex: _this.data.pageIndex,
      // pageSize: common.pageSize,
      lable: _this.data.lable,
      openId: app.globalData.openId
    },
    success: function (res, s, m) {
      _this.setData({
        // shopList: _this.data.shopList.concat(res),
        shopList: res,
      })
    },
    fail: function () { }
  })
}