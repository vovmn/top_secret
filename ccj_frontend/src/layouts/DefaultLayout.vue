<template>
  <MobileAppBar v-if="mobile" />
  <SideBar v-else />
  <v-main>
    <slot />
  </v-main>
</template>

<script lang="ts" setup>
  import { useDisplay, useTheme } from 'vuetify'
  import MobileAppBar from '@/components/shared/MobileAppBar.vue'
  import SideBar from '@/components/shared/SideBar.vue'

  const theme = useTheme()
  const { mobile } = useDisplay()

  onMounted(() => {
    const savedTheme = localStorage.getItem('app-theme') as 'light' | 'dark' | null
    if (savedTheme) {
      theme.change(savedTheme)
    }
  })
</script>
