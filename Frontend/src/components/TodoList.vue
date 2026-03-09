<template>
  <div class="ag-theme-alpine" style="height: 500px; width: 100%">
    <AgGridVue
      :suppressDragLeaveHidesColumns="true"
      :columnDefs="columnDefs"
      :rowData="todos"
      :defaultColDef="defaultColDef"
      style="height: 500px"
      @grid-ready="onGridReady"
      @cell-value-changed="onCellValueChanged"
    />
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { AgGridVue } from 'ag-grid-vue3'

// Register component automatically in <script setup>

// Column definitions
const columnDefs = ref([
  { field: 'id', sortable: true, filter: true },
  { field: 'title', sortable: true, filter: true },
  {
    field: 'completed',
    sortable: true,
    filter: true,
    editable: true,

    cellEditor: 'agSelectCellEditor',
    cellEditorParams: {
      values: [true, false],
    },
    valueSetter: (params) => {
      // This ensures Vue sees the change
      params.data.completed = params.newValue
      return true
    },
  },
  { field: 'priority ', sortable: true, filter: true, editable: true },
])

// Default column behavior
const defaultColDef = ref({
  flex: 1,
  minWidth: 100,
  resizable: true,
  editable: true,
})

// Row data
const todos = ref([
  { id: 1, title: 'Design login page', completed: true, priority: 'High' },
  { id: 2, title: 'Setup backend API', completed: true, priority: 'High' },
  { id: 3, title: 'Connect frontend to API', completed: false, priority: 'Medium' },
  { id: 4, title: 'Add authentication', completed: false, priority: 'High' },
  { id: 5, title: 'Create dashboard', completed: false, priority: 'Low' },
])

let gridApi = null
// Grid ready event
const onGridReady = (params) => {
  gridApi = params.api
  console.log('Grid ready:', params)

  // 🔥 expose to browser console
  window.gridApi = gridApi
}

const onCellValueChanged = (event) => {
  console.log('Cell value changed:', event)
}
</script>

<style></style>
