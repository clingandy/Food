using System;
using System.Collections.Generic;

namespace RandomOrderCore.Domains.View
{
    public class RfPhoneDeviceView
    {
        /// <summary>
        /// 
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 设备品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 设备型号
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// 微信设置的语言
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// 操作系统及版本
        /// </summary>
        public string System { get; set; }

        /// <summary>
        /// 微信版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 客户端平台
        /// </summary>
        public string Platform { get; set; }

    }
}
