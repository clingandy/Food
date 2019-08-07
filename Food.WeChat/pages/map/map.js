// pages/map/map.js
const app = getApp()
var common = require('../../utils/common.js')
// 引入SDK核心类
var QQMapWX = require('../../utils/qqmap-wx-jssdk.min.js');
// 实例化地图API核心类
var qqmapsdk = new QQMapWX({
  key: app.globalData.mapKey
});
var _key = 'chooseShopListCache'

Page({

  /**
   * 页面的初始数据
   */
  data: {
    centerAddrInfo: {},
    markers: [],
    markers_l:[],
    poi: {
      latitude: 23.106197,
      longitude: 113.323276
    },
    onAccelerometer: false,
    isSearch: true,
    pageIndex: 1,
    keyword: "",
    chooseShopListCache:[]
  },

  /**
   * 生命周期函数--监听页面加载
   */
  regionchange(e) {
    
  },

  /**
   * 点击标记的时候触发
   */
  markertap(e) {
    //console.log(e.markerId)
  },

  /**
   * 点击控件时触发
   */
  controltap(e) {
    //console.log(e.controlId)
  },

  // 设置中心点
  setCenterLocation: function (latitude, longitude){
    this.setData({
      poi: {
        latitude: latitude,
        longitude: longitude
      }
    });
  },
  // 获取中心点位置信息
  getCenterAddrInfo: function (latitude, longitude) {
    var _this = this;
    qqmapsdk.reverseGeocoder({
      //位置坐标，默认获取当前位置，非必须参数
      location: {
        latitude: latitude,
        longitude: longitude
      },
      // get_poi: 1, //是否返回周边POI列表：1.返回；0不返回(默认),非必须参数
      // poi_options: 'category=餐饮',
      success: function (res) {//成功后的回调
        console.log(res);
        var res = res.result;
        var mks = [];
        // for (var i = 0; i < res.poi_count; i++) {
        //   mks.push({ // 获取返回结果，放到mks数组中
        //       title: res.pois[i].title,
        //       id: res.pois[i].id,
        //       latitude: res.pois[i].location.lat,
        //       longitude: res.pois[i].location.lng,
        //       iconPath: '../../img/location_logo_red.png', //图标路径
        //       width: 30,
        //       height: 30
        //   })
        // }
        //当get_poi为0时或者为不填默认值时，检索目标位置，按需使用
        mks.push({ // 获取返回结果，放到mks数组中
          title: res.address,
          id: 0,
          latitude: res.location.lat,
          longitude: res.location.lng,
          iconPath: '../../img/location_logo_red.png',//图标路径
          width: 1,
          height: 1,
          callout: { //在markers上展示地址名称，根据需求是否需要
            content: res.formatted_addresses.recommend, //res.address + '\n'
            color: '#000',
            display: 'ALWAYS'
          }
        });
        _this.setData({ //设置markers属性和地图位置poi，将结果在地图展示
          centerAddrInfo: res,
          markers: mks
        });
      },
      fail: function (error) {
        console.error(error);
      },
      complete: function (res) {
        // console.log(res);
      }
    })
  },
  // 数据回填方法
  backfill: function (e) {
    var id = e.currentTarget.id;
    if (id <= this.data.suggestion.length && id >= 0){
      var suggestion = this.data.suggestion[id];
      this.setData({
        backfill: suggestion.title
      });
      this.setMarkers(suggestion);
    }
  },
  // 查询
  querySuggestion: function (e) {
    var _this = this;
    var keyword = e ? e.detail.value ? e.detail.value : '餐饮' : '餐饮';
    _this.setData({ //设置suggestion属性，将关键词搜索结果以列表形式展示
      keyword: keyword,
      pageIndex: 1
    });
    _this.getSuggestion(false);
  },
  // 获取数据列表信息
  getSuggestion: function(noFirst){
    var _this = this;
    if (!_this.data.isSearch)
      return;
    var pIndex = _this.data.pageIndex;
    if (pIndex > 3) {
      common.warn('没有更多数据');
      return;
    }
    // 先获取中心位置信息
    _this.mapCtx.getCenterLocation({
      success: function (res) {
        //调用关键词提示接口
        qqmapsdk.getSuggestion({
          //获取输入框值并设置keyword参数
          keyword: _this.data.keyword ? _this.data.keyword : '餐饮', //用户输入的关键词，可设置固定值,如keyword:'KFC'
          region: _this.data.centerAddrInfo && _this.data.centerAddrInfo.ad_info ? _this.data.centerAddrInfo.ad_info.city : '', //设置城市名，限制关键词所示的地域范围，非必填参数
          filter: encodeURI('category=餐饮'),
          location: res.latitude + ',' + res.longitude,
          page_index: pIndex,
          page_size: 20,
          success: function (res) {//搜索成功后的回调
            console.log(res);
            if (res.data.length == 0){
              common.warn('没有更多数据');
              return;
            }
            var sug = [];
            for (var i = 0; i < res.data.length; i++) {
              sug.push({ // 获取返回结果，放到sug数组中
                title: res.data[i].title,
                idStr: res.data[i].id,
                category: res.data[i].category,
                type: res.data[i].type,
                adcode: res.data[i].adcode,
                address: res.data[i].address,
                province: res.data[i].province,
                city: res.data[i].city,
                district: res.data[i].district,
                latitude: res.data[i].location.lat,
                longitude: res.data[i].location.lng,
                distance: res.data[i]._distance
              });
            }
            if (noFirst){
              _this.setData({
                pageIndex: _this.data.pageIndex + 1,
                suggestion: _this.data.suggestion.concat(sug)
              });
            }else{
              _this.setData({
                pageIndex: _this.data.pageIndex + 1,
                suggestion: sug
              });
            }
            _this.saveShopList(sug);
          },
          fail: function (error) {
            console.error(error);
            common.alertError('获取数据失败！');
          },
          complete: function (res) {
            // console.log(res);
          }
        });
      }
    })
  },
  // 获取当前地图中心的经纬度
  getCenterLocation: function () {
    var _this = this;
    _this.mapCtx.getCenterLocation({
      success: function (res) {
        _this.setCenterLocation(res.latitude, res.longitude);
        // 调用地图接口
        _this.getCenterAddrInfo(res.latitude, res.longitude);
      }
    })
  },
  // 将地图中心移动到当前定位点
  moveToLocation: function () {
    this.mapCtx.moveToLocation()
  },
  // 平移marker，带动画
  translateMarker: function (latitude, longitude) {
    var _this = this;
    _this.mapCtx.translateMarker({
      markerId: 0,
      autoRotate: false,
      duration: 1000,
      destination: {
        latitude: latitude,
        longitude: longitude,
      },
      animationEnd() {
        
      }
    })
  },
  // 缩放视野展示所有经纬度
  includePoints: function () {
    this.mapCtx.includePoints({
      padding: [10],
      points: [{
        latitude: 23.10229,
        longitude: 113.3345211,
      }, {
        latitude: 23.00229,
        longitude: 113.3345211,
      }]
    })
  },
  // 获取当前地图的缩放级别
  scaleClick: function () {
    this.mapCtx.getScale({
      success: function (res) {
        console.log(res.scale)
      }
    })
  },
  // 获取当前地图的视野范围
  getRegionClick: function () {
    this.mapCtx.getRegion({
      success: function (res) {
        console.log(res.southwest)
        console.log(res.northeast)
      }
    })
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    this.getSetting();

    var cache = common.getCache(_key);
    if (!cache) {
      cache = [];
    }
    this.setData({
      chooseShopListCache: cache,
    })
  },

  /**
   * 地理位置授权 和 获取地理信息
   */
  getSetting : function(){
    wx.getSetting({
      success: (res) => {
        // console.log(JSON.stringify(res))
        // res.authSetting['scope.userLocation'] == undefined    表示 初始化进入该页面
        // res.authSetting['scope.userLocation'] == false    表示 非初始化进入该页面,且未授权
        // res.authSetting['scope.userLocation'] == true    表示 地理位置授权
        if (res.authSetting['scope.userLocation'] != undefined && res.authSetting['scope.userLocation'] != true) {
          wx.showModal({
            title: '请求授权当前位置',
            content: '需要获取您的地理位置，请确认授权',
            success: function (res) {
              if (res.cancel) {
                wx.showToast({
                  title: '拒绝授权',
                  icon: 'none',
                  duration: 1000
                })
              } else if (res.confirm) {
                wx.openSetting({
                  success: function (dataAu) {
                    if (dataAu.authSetting["scope.userLocation"] == true) {
                      wx.showToast({
                        title: '授权成功',
                        icon: 'success',
                        duration: 1000
                      })
                      //再次授权，调用wx.getLocation的API

                    } else {
                      wx.showToast({
                        title: '授权失败',
                        icon: 'none',
                        duration: 1000
                      })
                    }
                  }
                })
              }
            }
          })
        } else if (res.authSetting['scope.userLocation'] == undefined) {
          //调用wx.getLocation的API
          this.getLocation();
        }
        else {
          //调用wx.getLocation的API
          this.getLocation();
        }
      }
    })
  },

  /**
   * 获取当前地理信息
   */
  getLocation: function(){
    var _this = this;
    wx.getLocation({
      type: 'gcj02',
      success(res) {
        _this.setCenterLocation(res.latitude, res.longitude)
        setTimeout(_this.moveToLocation, 300)
        setTimeout(_this.getSuggestion, 600)
        // setTimeout(_this.getCenterLocation, 600);
      }
    })
  },

  /**
   * 生命周期函数--监听页面初次渲染完成
   */
  onReady: function () {
    //创建 map 上下文 MapContext 对象。
    this.mapCtx = wx.createMapContext('myMap')
  },

  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function () {
  },

  // 设置suggestion的Marker并且平移
  setMarkers: function (suggestion){
    var _this = this;
    var mks = [];
    mks.push({ // 获取返回结果，放到mks数组中
      title: suggestion.title,
      id: 0,
      latitude: suggestion.latitude,
      longitude: suggestion.longitude,
      iconPath: '../../img/location_logo_red.png',//图标路径
      width: 42,
      height: 42,
      callout: { //在markers上展示地址名称，根据需求是否需要
        content: suggestion.title, //res.address + '\n'
        color: '#d81e06',
        display: 'ALWAYS'
      }
    });
    _this.setData({ //设置markers属性和地图位置poi，将结果在地图展示
      markers: mks,
      backfill: suggestion.title
    });
    _this.setCenterLocation(suggestion.latitude, suggestion.longitude);
  },

  // 保存地图接口查询数据
  saveShopList: function(list){
    common.postData({
      url: "/RfCateringShop/AddBatchRfCateringShop",
      isNoFail: true,
      isNoLoading: true,
      params: list,
      success: function (res, s, m) {

      },
      fail: function () {

      }
    })
  },

  // 添加收藏
  addShopCollection: function(e){
    var _this = this;
    var id = e.currentTarget.id;
    if (id <= _this.data.suggestion.length && id >= 0) {
      var sug = _this.data.suggestion[id];
      var shop = {
        title: sug.title,
        category: sug.category,
        lable: 8,
        openId: app.globalData.openId
      }
      common.postData({
        url: "/RfMemberCustomizeShop/AddRfMemberCustomizeShop",
        params: shop,
        success: function (res, s, m) {
          if (s && res > 0) {
            common.alert('收藏成功！')
            // wx.navigateBack({})
          } else {
            common.alertError('收藏失败！')
          }
        },
        fail: function () {
        }
      })
    }
  },

  // 添加数据到缓存
  addShopCache: function (e){
    var _this = this;
    var id = e.currentTarget.id;
    if (id <= _this.data.suggestion.length && id >= 0) {
      var sug = _this.data.suggestion[id];
      var cache = common.getCache(_key);
      if (!cache){
        cache = [];
      }
      if (cache.findIndex((element) => (element.idStr == sug.idStr)) >= 0){
        return;
      }
      cache = cache.concat(sug),
      common.setCache(_key, cache);
      _this.setData({
        chooseShopListCache: cache,
      })
    }
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
   * scroll-view触底事件的处理函数
   */
  bindscrolltolower: function(){
    this.getSuggestion(true);
  },

  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function () {

  }
})