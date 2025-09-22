<template>
	<v-text-field v-model="model" label="Поиск по объектам" variant="outlined" density="comfortable" clearable
		hide-details prepend-inner-icon="mdi-magnify"></v-text-field>
</template>

<script setup lang="ts">
const model = defineModel<string>()
</script>

<style scoped lang="sass">

</style>
