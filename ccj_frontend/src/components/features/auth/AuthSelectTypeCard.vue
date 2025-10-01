<template>
  <v-hover>
    <template #default="{ isHovering, props }">
      <v-card
        class="cursor-pointer"
        v-bind="props"
        :color="isHovering ? 'info' : 'primary'"
        :title="title"
        :to="to"
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
    to: string
  }>()
</script>

<style scoped lang="sass">

</style>
