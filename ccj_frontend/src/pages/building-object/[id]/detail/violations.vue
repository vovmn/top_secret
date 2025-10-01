<template>
  <DefaultLayout>
    <BaseContainer>
      <h1 class="font-weight-bold mb-8">Нарушения</h1>
      <v-row justify="space-between">
        <v-col
          class="mb-8"
          cols="12"
          lg="12"
          md="12"
          sm="12"
        >
          <v-data-table
            class="rounded-sm"
            :headers="headersViolations"
            hover
            item-value="product"
            :items="checkListData"
            :items-per-page="10"
          >
            <template #item.workStoppage="{ value }">
              <v-chip v-if="value" class="rounded-sm" close color="error">Остановка работы</v-chip>
            </template>
            <template #item.commitData="{ value }">
              <v-btn color="info" size="small" :to="value" variant="tonal">Добавить</v-btn>
            </template>
          </v-data-table>
        </v-col>
      </v-row>
    </BaseContainer>
  </DefaultLayout>
</template>

<script setup lang="ts">
  import { headersViolations } from '@/components/features/building_object/constants'
  import DefaultLayout from '@/layouts/DefaultLayout.vue'

  const checkListData = [
    {
      category: 'Категория 1',
      id: '1',
      view: 'Вид',
      type: 'Тип',
      name: 'Название',
      deadline: '01.01.2026',
      dateReview: '15.11.2025',
      workStoppage: false,
      addition: 'Дополнение',
      commitData: '/',
    },
    {
      category: 'Категория 1',
      id: '1',
      view: 'Вид',
      type: 'Тип',
      name: 'Название',
      deadline: '01.01.2026',
      dateReview: '15.11.2025',
      workStoppage: false,
      addition: 'Дополнение',
      commitData: '/',
    },
    {
      category: 'Категория 1',
      id: '1',
      view: 'Вид',
      type: 'Тип',
      name: 'Название',
      deadline: '01.01.2026',
      dateReview: '15.11.2025',
      workStoppage: true,
      addition: 'Дополнение',
      commitData: '/',
    },
    {
      category: 'Категория 2',
      id: '1',
      view: 'Вид',
      type: 'Тип',
      name: 'Название',
      deadline: '01.01.2026',
      dateReview: '15.11.2025',
      workStoppage: false,
      addition: 'Дополнение',
      commitData: '/',
    },
    {
      category: 'Категория 3',
      id: '1',
      view: 'Вид',
      type: 'Тип',
      name: 'Название',
      deadline: '01.01.2026',
      dateReview: '15.11.2025',
      workStoppage: false,
      addition: 'Дополнение',
      commitData: '/',
    },
  ]
</script>

<style scoped lang="sass">

</style>
