<template>
  <v-hover>
    <template #default="{ isHovering, props }">
      <v-card
        class="cursor-pointer"
        v-bind="props"
        :color="isHovering ? 'info' : 'primary'"
        :title="title"
        @click="$emit('click')"
      >
        <v-card-text>
          <v-img :src="src" />
        </v-card-text>
      </v-card>
    </template>
  </v-hover>
</template>

<script setup lang="ts">
  defineProps<{
    title: string
    src: string
  }>()

  defineEmits<{
    (e: 'click'): void
  }>()
</script>

<style scoped lang="sass">

</style>
