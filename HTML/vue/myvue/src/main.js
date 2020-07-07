import Vue from 'vue'
import App from './App'
import router from './router' // 自动扫描里面的路由配置

Vue.config.productionTip = false;

// 显示声明使用 VueRouter
// Vue.use(VueRouter);

/* eslint-disable no-new */
new Vue({
  el: '#app',
  // 让这个 Vue 对象里面有一个路由
  // 配置路由，而所有的路由配置均在 ./router/index.js 中
  router,
  components: { App },
  template: '<App/>'
});
