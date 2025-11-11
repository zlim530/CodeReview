<script setup>
import { ref } from 'vue';
import myButton from './components/button.vue';
import axios from 'axios';

const value = ref('');
const list = ref([
  { text: '上午面试', isCompleted: false },
  { text: '中午吃饭', isCompleted: false },
  { text: '晚上看电影', isCompleted: false },
]);

function add() {
  list.value.push({
    text: value.value,
    isCompleted: false,
  });

  value.value = '';
}

function del(index) {
  list.value.splice(index, 1);
}

function print(str) {
  console.log(str);
}

// Axios example
// 获取远程数据
// 模拟从服务器获取待办事项列表
// getList();
async function getList(){
  const res = await axios({
    url:'https://jsonplaceholder.typicode.com/todos',
    // url:'https://q6zv39.laf.run/get_list',
    method:'get'
  });

  list.value = res.data.list;
  console.log(res);
}

async function addFromServer(){
  await axios({
    url:'https://jsonplaceholder.typicode.com/todos',
    // url:'https://q6zv39.laf.run/add_todo',
    method:'post',
    data:{
      text:value.value,
      isCompleted:false,
    }
  });

  getList();
}

async function onUpdated(id){
  await axios({
    url:'https://jsonplaceholder.typicode.com/todos',
    // url:'https://q6zv39.laf.run/update_todo',
    method:'post',
    data:{
      id,
    }
  });

  getList();
}


async function deleteTodo(id){
  await axios({
    url:'https://jsonplaceholder.typicode.com/todos',
    // url:'https://q6zv39.laf.run/det_todo',
    method:'post',
    data:{
      id: id,
    }
  });

  getList();
}

</script>


<template>
  <div class="todo-app">
    <div class="title">Todo App</div>

    <div class="todo-form">
      <input v-model="value"
        type="text"
        class="todo-input"
        placeholder="Add a todo"
      />
      <div @click="add" class="todo-button">Add Todo</div>
    </div>

    <div v-for="(item,index) in list" :class="[item.isCompleted ? 'completed' : 'item']">
      <div>
        <!-- 看后端接口传回来的数据中唯一标识符是怎么命名的即可 -->
        <input @click="onUpdated(item._id)" v-model="item.isCompleted" type="checkbox" />
        <span class="name">{{ item.text }}</span>
      </div>

      <div @click="del(index)" class="del">del</div>
      <!-- <div @click="del(item._id)" class="del">del</div> -->
    </div>
  </div>

  <my-button @ok="print" text="点击我"></my-button>
  <my-button text="点击你"></my-button>
  <my-button text="点击她"></my-button>
</template>

<style scoped>
.todo-app {
  box-sizing: border-box;
  margin-top: 40px;
  margin-left: 1%;
  padding-top: 30px;
  width: 98%;
  height: 500px;
  background: #ffffff;
  border-radius: 5px;
}

.title {
  text-align: center;
  font-size: 30px;
  font-weight: 700;
}

.todo-form {
  display: flex;
  margin: 20px 0 30px 20px;
}

.todo-button {
  width: 100px;
  height: 52px;
  border-radius: 0 20px 20px 0;

  text-align: center;
  background: linear-gradient(
    to right,
    rgb(113, 65, 168),
    rgba(44, 114, 251, 1)
  );
  color: #fff;
  line-height: 52px;
  cursor: pointer;
  font-size: 14px;
  user-select: none;
}

.todo-input {
  padding: 0px 15px 0px 15px;
  border-radius: 20px 0 0 20px;
  border: 1px solid #dfe1e5;
  outline: none;
  width: 60%;
  height: 50px;
}

.item {
  box-sizing: border-box;
  display: flex;
  align-items: center;
  justify-content: space-between;
  width: 80%;
  height: 50px;
  margin: 8px auto;
  padding: 16px;
  border-radius: 20px;
  box-shadow: rgba(149, 157, 165, 0.2) 0px 8px 20px;
}

.del {
  color: red;
}

.completed {
  box-sizing: border-box;
  display: flex;
  align-items: center;
  justify-content: space-between;
  width: 80%;
  height: 50px;
  margin: 8px auto;
  padding: 16px;
  border-radius: 20px;
  box-shadow: rgba(149, 157, 165, 0.2) 0px 8px 20px;
  text-decoration: line-through;
  opacity: 0.4;
}
</style>
