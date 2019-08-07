// pages/choose/food/food.js
const app = getApp()
const common = require('../../../utils/common.js')
const _key = "chooseFoodListCache"
let categoryTemp = [{  id: -1,  name: "全部",  pId: 3,}]
Page({

  /**
   * 页面的初始数据
   */
  data: {
    canIUse: wx.canIUse('button.open-type.getUserInfo'),

    isSearch: true,
    pageIndex: 1,
    foodList: [],

    categoryStr:"菜谱 家常菜谱",
    categoryStr0: "菜谱",
    categoryStr1: "家常菜谱",
    categoryStr2: "",

    imgUrl: common.imgUrl,
    name: "",
    multiIndex: [2, 0, 0],
    category: 32,
    categoryTemp: 32,
    categoryArray: [
      [],[],[]
    ],
    categoryList:[]
  },

  // 添加缓存
  addFoodCache: function(e){
    var _this = this;
    var id = e.currentTarget.id;
    if (id <= _this.data.foodList.length && id >= 0) {
      var food = _this.data.foodList[id];
      var cache = common.getCache(_key);
      if (!cache) {
        cache = [];
      }
      if (cache.findIndex((element) => (element.id == food.id)) >= 0) {
        return;
      }
      cache = cache.concat(food),
      common.setCache(_key, cache);
      _this.setData({
        chooseFoodListCache: cache,
      })
      _this.addSort(e);
    }
  },

  // 权重升级+1
  addSort: function(e){
    var _this = this;
    var id = e.currentTarget.id;
    if (id <= _this.data.foodList.length && id >= 0) {
      var food = _this.data.foodList[id];
      var id = food.id;
      var sort = food.sort + 1;
      common.postData({
        method: "PUT",
        isNoFail: true,
        isNoLoading: true,
        url: "/RfFood/ModifyRfFoodSort?id=" + id + "&sort=" + sort,
        params: {},
        success: function (res, s, m) { },
        fail: function () { }
      })
    }
  },

  //查询
  queryFood: function(e){
    this.setData({
      foodList: [],
      isSearch: true,
      pageIndex: 1,
      name: e.detail.value ? e.detail.value : ''
    })
    searchList(this);
  },

  // 分类选择
  bindCategoryChange: function (e) {
    if (this.data.categoryTemp == this.data.category){
      return;
    }
    let categoryStr = this.data.categoryStr0 + "  " + this.data.categoryStr1 + "  " + this.data.categoryStr2;
    this.setData({
      category: this.data.categoryTemp,
      categoryStr: categoryStr,
      foodList: [],
      isSearch: true,
      pageIndex: 1
    })
    searchList(this);
  },

  // 分类选择
  bingCategoryColumnChange: function(e){
    //let index = this.data.multiIndex;
    let data = {
      categoryArray: this.data.categoryArray,
      multiIndex: this.data.multiIndex,
      categoryTemp: this.data.categoryTemp,
      categoryStr0: this.data.categoryStr0,
      categoryStr1: this.data.categoryStr1,
      categoryStr2: this.data.categoryStr2,
    }

    let pInfo = data.categoryArray[e.detail.column][e.detail.value];
    if(!pInfo){
      return;
    }
    data.categoryTemp = pInfo.id;

    if (e.detail.column == 0) {
      data.categoryStr0 = pInfo.name;
      data.categoryStr1 = "";
      data.categoryStr2 = "";
      data.categoryArray[1] = this.data.categoryList.filter(t => t.pId == pInfo.id);

      categoryTemp[0].id = pInfo.id;
      categoryTemp[0].pId = pInfo.id;
      data.categoryArray[1] = categoryTemp.concat(data.categoryArray[1])

      data.categoryArray[2] = [];
      // if (data.categoryArray[1].length > 0){
      //   data.categoryArray[2] = this.data.categoryList.filter(t => t.pId == data.categoryArray[1][0].id);
      // }
      data.multiIndex[1] = 0;
      data.multiIndex[2] = -1;
    } else if (e.detail.column === 1) {
      data.categoryStr1 = pInfo.name;
      data.categoryStr2 = "";
      data.categoryArray[2] = this.data.categoryList.filter(t => t.pId == pInfo.id);
      categoryTemp[0].id = pInfo.id;
      categoryTemp[0].pId = pInfo.id;
      data.categoryArray[2] = categoryTemp.concat(data.categoryArray[2])
      data.multiIndex[2] = 0;
    } 
    else if (e.detail.column === 2) {
      data.categoryStr2 = pInfo.name;
    } 
    data.multiIndex[e.detail.column] = e.detail.value;

    this.setData(data);
  },

  // 当前条件随机
  randChooseFood: function(){
    const _this = this;
    if (_this.data.foodList.length == 0){
      common.warn('没有数据支持')
      return;
    }
    common.getData({
      url: "/RfFood/GetRfFoodByRand",
      params: {
        category: _this.data.category,
        name: _this.data.name,
        openId : app.globalData.openId
      },
      success: function (res, s, m) {
        if (s && res > 0) {
          wx.navigateTo({
            url: '/pages/choose/foodDetails/foodDetails?id=' + res +'&name=详情',
          })
        } else {
          common.alertError('随机选择失败')
        }
      },
      fail: function () { }
    })
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    getCategoryList(this);
    searchList(this);

    var cache = common.getCache(_key);
    if (!cache) {
      cache = [];
    }
    this.setData({
      chooseFoodListCache: cache,
    })

    // wx.showShareMenu({
    //   withShareTicket: true
    // })
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
    this.setData({
      foodList: [],
      isSearch: true,
      pageIndex: 1
    })
    searchList(this);
  },

  /**
   * 页面上拉触底事件的处理函数
   */
  onReachBottom: function () {
    searchList(this);
  },

  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function () {

  }
})

function getCategoryList(_this) {
  common.getData({
    url: "/RfFoodCategory/GetRfFoodCategoryAll",
    params: {},
    success: function (res, s, m) {
      if (!s){
        return;
      }
      var categoryArray = _this.data.categoryArray;
      categoryArray[0] = res.filter(t => t.pId == 0)
      if (categoryArray[0].length > 2){
        categoryArray[1] = res.filter(t => t.pId == categoryArray[0][2].id);
      }
      if (categoryArray[1].length > 0) {
        categoryArray[2] = res.filter(t => t.pId == categoryArray[1][0].id);
        categoryTemp[0].id = categoryArray[1][0].id;
        categoryTemp[0].pId = categoryArray[1][0].id;
        categoryArray[2] = categoryTemp.concat(categoryArray[2])
      }
      _this.setData({
        categoryList: res,
        categoryArray: categoryArray
      })
    },
    fail: function () { }
  })
}


function searchList(_this) {
  if (!_this.data.isSearch)
    return;
  common.getData({
    url: "/RfFood/GetRfFoodPageList",
    params: {
      pageIndex: _this.data.pageIndex,
      pageSize: common.pageSize,
      category: _this.data.category,
      name: _this.data.name
    },
    success: function (res, s, m) {
      if (s && res.length != 0) {
        _this.setData({
          foodList: _this.data.foodList.concat(res),
          pageIndex: _this.data.pageIndex + 1,
        })
        if (res.length != common.pageSize) {
          _this.setData({
            isSearch: false,
          })
        }
      } else {
        _this.setData({
          isSearch: false,
        })
      }
    },
    fail: function () { }
  })
}