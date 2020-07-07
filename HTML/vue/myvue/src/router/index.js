import Vue from "vue";
// 导入路由插件
import VueRouter from "vue-router";

// 导入自定义的组件
import Content from "../components/Content";
import Main from "../components/Main";
// 2.导入新建的 Zlim.vue 组件，并命名为 ZLim
import ZLim from "../components/Zlim"

// 安装路由
Vue.use(VueRouter);

// 配置路由
export default new VueRouter({
  // 中括号 [] 表示数组，花括号 {} 表示对象
  routes:[
    {
      //  路由路径
      // 表示后续访问 /content 页面就会进入 Content 组件
      path:'/content',
      // 路由名称
      name:'content',
      //  跳转到组件
      component:Content
    },
    {
      //  路由路径
      path:'/main',
      // 路由名称
      name:'main',
      //  跳转的组件
      component:Main
    },
    {
      //  3.配置 zlim 组件路径
      path:'/zlim',
      // 路由名称
      name:'main',
      //  跳转的组件
      component:ZLim
    }
  ]
});
