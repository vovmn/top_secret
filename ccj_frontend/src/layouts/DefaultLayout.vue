<template>
  <SideBar />
  <v-main>
    <slot />
  </v-main>
</template>

<script lang="ts" setup>
  import { useTheme } from 'vuetify'
  import SideBar from '@/components/shared/SideBar.vue'

  const theme = useTheme()

  onMounted(() => {
    const savedTheme = localStorage.getItem('app-theme') as 'light' | 'dark' | null
    if (savedTheme) {
      theme.change(savedTheme)
    }
  })
</script>
