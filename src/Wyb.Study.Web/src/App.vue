<script setup>
import { RouterLink, RouterView } from 'vue-router'
</script>

<template>
  <div class="common-layout">
    <el-container>
      <el-header>
        <el-menu
          :default-active="activeIndex"
          class="el-menu-demo"
          mode="horizontal"
          @select="handleSelect"
        >
          <el-menu-item index="1">首页</el-menu-item>
        </el-menu>
      </el-header>
      <el-container>
        <el-aside class="left-nav">
          <el-menu
            default-active="2"
            class="el-menu-vertical-demo"
            @open="handleOpen"
            @close="handleClose"
            router
          >
            <el-sub-menu index="1">
              <template #title>
                <el-icon><location /></el-icon>
                <span>基础信息</span>
              </template>
              <el-menu-item index="/basic/function">功能列表</el-menu-item>
              <el-menu-item index="1-2">用户管理</el-menu-item>
              <el-menu-item index="1-3">角色管理</el-menu-item>
              <el-menu-item index="1-4">用户角色管理</el-menu-item>
              <el-menu-item index="1-5">角色功能管理</el-menu-item>
            </el-sub-menu>
            <el-menu-item index="4">
              <el-icon><setting /></el-icon>
              <span>日志查询</span>
            </el-menu-item>
          </el-menu>
        </el-aside>
        <el-main>
          <RouterView />
        </el-main>
      </el-container>
    </el-container>
  </div>
</template>

<style scoped>

</style>

<script>
  import {
    Document,
    Menu as IconMenu,
    Location,
    Setting,
  } from '@element-plus/icons-vue'
</script>
