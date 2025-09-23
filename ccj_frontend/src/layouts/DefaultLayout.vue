<template>
  <SideBar />
  <v-main>
    <slot />
  </v-main>
</template>

<script lang="ts" setup>
  import SideBar from '@/components/shared/SideBar.vue'
import { useTheme } from 'vuetify'

const theme = useTheme()

  onMounted(() => {
    const savedTheme = localStorage.getItem('app-theme') as 'light' | 'dark' | null
    if (savedTheme) {
      theme.global.name.value = savedTheme
    }
  })
</script>
