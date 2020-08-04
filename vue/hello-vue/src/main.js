import Vue from 'vue'
import App from './App'

import router from './router'

// 导入 ElementUI
import ElementUI from 'element-ui'
import 'element-ui/lib/theme-chalk/index.css';

// cnpm:有可能安装失败，安装不上就用 npm 试一试
import axios from 'axios';
import VueAxios from 'vue-axios';

Vue.use(VueAxios,axios);

Vue.use(router);
Vue.use(ElementUI);

new Vue({
  el: '#app',
  router,
  render: h => h(App)// ElementUI 官网快速上手
});
