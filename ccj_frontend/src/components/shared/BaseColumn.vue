<template>
  <v-col
    :class="classList"
    cols="12"
    :lg="cols"
    :md="cols"
    sm="12"
  >
    <slot />
  </v-col>
</template>

<script setup lang="ts">
  defineProps({
    cols: { type: String, required: true },
    classList: { type: String, default: '' },
  })
</script>

<style scoped lang="sass">

</style>
