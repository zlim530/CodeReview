<template>
  <!--所有的元素(数据)，必须不能直接在根节点下-->
  <div>
    <h1>个人信息</h1>
    <!-- 三、在组件中取出传入的参数，展示给用户，数据必须包含在标签中，否则会报错 -->
    <!--{{ $route.params.id }}-->
    {{id}} <!--方式2：通过 props 来解耦并取参数-->
  </div>
</template>

<script>
    export default {
        props:['id'],
        name: "UserProfile",
        // 就像过滤器/拦截器一样
        beforeRouteEnter:(to,from,next) => {
          console.log("进入路由之前");
          // 在进入页面之前加载数据
          next( vm => {
            vm.getData();// 进入路由之前执行 getData 方法
          });
        },
        beforeRouteLeave:(to,from,next) => {
          console.log("进入路由之后");
          next();
        },
        methods:{
          getData:function () {
            this.axios({
              method:'get',
              url:'http://localhost:8080/static/mock/data.json'
            }).then(function (response) {
              console.log(response);
            });
          }
        }
    }
</script>

<style scoped>

</style>
