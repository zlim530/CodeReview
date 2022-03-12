import Vue from 'vue'
import Router from 'vue-router'

import Main from '../views/Main'
import Login from '../views/Login'

import UserList from '../views/user/List'
import UserProfile from '../views/user/Profile'
import NotFound from "../views/NotFound";

Vue.use(Router);

export default new Router({
  mode:'history',
  routes:[
    {
      // path:'/main/:name',
      path:'/main',
      name:'Main',
      component:Main,// 嵌套路由
      props:true,
      children:[
        // 二、路由接收：/:id 表示接收前端传来的 id 参数：注意：此参数名需要与前端传递过来的参数名一致
        { path:'/user/profile/:id',name:'UserProfile',component:UserProfile,props:true },
        { path: '/user/list',component: UserList}
      ]
    },
    {
      path: '/login',
      name:'Login',
      component: Login
    },
    {
      path:'/goHome',
      // 路由重定向
      redirect:'/main'
    },
    {
      path:'*',
      component:NotFound
    }
  ]
});
