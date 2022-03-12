<template>
  <div id="app">

    <h1>Vue-Router</h1>
    <!--
      router-link: 默认会被渲染成一个 <a> 标签, to 属性为指定链接
      router-view: 用于渲染路由匹配到的组件(内容)
    -->
    <router-link to="/main">首页</router-link>
    <router-link to="/content">内容页</router-link>
<!--    4.使用配置好的路由,并跳转到 Zlim 组件-->
    <router-link to="/zlim">ZLIM</router-link>
    <!--    router-view 展示 template 模板，也即自定义组件中的内容-->
    <router-view></router-view>

  </div>
</template>

<script>
import Content from "./components/Content";

export default {
  name: 'App',
  comments:{
    Content
  }
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
