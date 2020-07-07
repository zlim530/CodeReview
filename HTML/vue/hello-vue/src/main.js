import Vue from 'vue'
import App from './App'

import router from './router'

// 导入 ElementUI
import ElementUI from 'element-ui'
import 'element-ui/lib/theme-chalk/index.css';


Vue.use(router);
Vue.use(ElementUI);

new Vue({
  el: '#app',
  router,
  render: h => h(App)// ElementUI 官网快速上手
});
