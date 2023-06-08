<template>
  <main>
    <el-form :inline="true" :model="queryModel" class="demo-form-inline">
      <el-form-item label="功能名称">
        <el-input v-model="queryModel.functionName" placeholder="功能名称" />
      </el-form-item>
      <el-form-item label="功能路由地址">
        <el-input v-model="queryModel.functionUrl" placeholder="功能路由地址" />
      </el-form-item>
      <el-form-item>
        <el-button type="primary" @click="onSubmit">查询</el-button>
      </el-form-item>
    </el-form>
  </main>
</template>

<style scoped>
</style>

<script setup>
  import { reactive } from 'vue'

  const queryModel = {
    functionName:"",
    functionUrl:""
  };

  const onSubmit = () => {
    console.log('submit!')
  }
</script>