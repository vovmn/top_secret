<!-- GanttChart.vue -->
<template>
  <div ref="ganttContainer" />
</template>

<script setup>
  import { onMounted, onUnmounted, ref, watch } from 'vue'

  const props = defineProps({
    tasks: {
      type: Array,
      required: true,
    },
    onClick: {
      type: Function,
      default: () => {},
    },
    onDateChange: {
      type: Function,
      default: () => {},
    },
    onProgressChange: {
      type: Function,
      default: () => {},
    },
    onViewModeChange: {
      type: Function,
      default: () => {},
    },
  })

  const ganttContainer = ref(null)
  let ganttInstance = null

  onMounted(async () => {
    const container = ganttContainer.value
    if (!container) return

    try {
      const module = await import('frappe-gantt')

      const Gantt = module.default

      ganttInstance = new Gantt(container, props.tasks, {
        on_click: props.onClick,
        on_date_change: props.onDateChange,
        on_progress_change: props.onProgressChange,
        on_view_mode_change: props.onViewModeChange,
        language: 'ru',
        arrow_curve: 10,
        bar_corner_radius: 5,
        bar_height: 40,
        container_height: 1000,
        column_width: 50,
        date_format: 'YYYY-MM-DD',
        upper_header_height: 50,
        lower_header_height: 35,
        snap_at: '1d',
        infinite_padding: true,
        language: 'ru',
        lines: 'both',
        move_dependencies: true,
        padding: 20,
        popup_on: 'click',
        readonly: false,
        readonly_dates: false,
        readonly_progress: false,
        scroll_to: 'today',
        show_expected_progress: true,
        today_button: true,
        view_mode: 'Day',
        view_mode_select: true,
        auto_move_label: true,
      })
    } catch (error) {
      console.error('Failed to load or initialize frappe-gantt:', error)
    }
  })

  watch(
    () => props.tasks,
    newTasks => {
      if (ganttInstance) {
        ganttInstance.changeData(newTasks)
      }
    },
    { deep: true },
  )

  watch(
    () => props.viewMode,
    mode => {
      if (ganttInstance && mode) {
        ganttInstance.changeViewMode(mode)
      }
    },
  )

  onUnmounted(() => {
    if (ganttInstance) {
      ganttInstance.clear()
    }
  })
</script>

<style scoped></style>
