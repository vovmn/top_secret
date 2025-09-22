<template>
  <SideBar />
  <v-main>
    <slot />
  </v-main>
</template>

<script lang="ts" setup>
import SideBar from '@/components/shared/SideBar.vue';
</script>
