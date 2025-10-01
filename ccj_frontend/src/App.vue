<template>
  <v-app>
    <router-view />
  </v-app>
</template>

<script lang="ts" setup>
  import { useAuthStore } from './stores/app'

  const authStore = useAuthStore()

  onMounted(() => {
    authStore.restoreSession()
  })
</script>
