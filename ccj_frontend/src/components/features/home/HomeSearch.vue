<template>
  <v-text-field
    v-model="model"
    clearable
    density="comfortable"
    hide-details
    label="Поиск по объектам"
    prepend-inner-icon="mdi-magnify"
    variant="outlined"
  />
</template>

<script setup lang="ts">
  const model = defineModel<string>()
</script>

<style scoped lang="sass">

</style>
