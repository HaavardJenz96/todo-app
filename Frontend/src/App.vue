<script setup lang="ts">
import Header from './components/Header.vue'
import TodoList from './components/TodoList.vue'
import Grid from './components/TodoList.vue'

                                              import { AllCommunityModule, ModuleRegistry } from 'ag-grid-community'

// Register all Community features
ModuleRegistry.registerModules([AllCommunityModule])
</script>

<template>
  <Header></Header>
  <TodoList />
</template>

<style scoped></style>
