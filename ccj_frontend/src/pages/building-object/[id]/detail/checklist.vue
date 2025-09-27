<template>
  <DefaultLayout>
    <BaseContainer>
      <h1 class="mb-8 font-weight-bold">Статус работы</h1>
      <v-row justify="space-between">
        <v-col cols="12" sm="12" md="7" lg="7">
          <h2 class="mb-4 text-grey-lighten-1">Чек-Лист</h2>
          <v-data-table class="rounded-sm" :headers="Сonstants.headersCheckList" :items="checkListData"
            :groupBy="Сonstants.checkListGroupBy" :items-per-page="10" item-value="product" hover>
            <template v-slot:item.status="{ value }">
              <v-chip class="rounded-sm" close>{{ value }}</v-chip>
            </template>
          </v-data-table>
        </v-col>
      </v-row>
    </BaseContainer>
  </DefaultLayout>
</template>

<script setup lang="ts">
import * as Сonstants from '@/components/features/building_object/constants/index';
import DefaultLayout from '@/layouts/DefaultLayout.vue';

const checkListData = [
  {
    data: 'Положить асфальт',
    numObject: '1',
    deadline: '02.01.2026',
    status: 'Не выполнено'
  },
  {
    data: 'Положить бекенд',
    numObject: '1',
    deadline: '02.01.2026',
    status: 'Не выполнено'
  },
  {
    data: 'Положить фронтенд',
    numObject: '1',
    deadline: '03.01.2026',
    status: 'Не выполнено'
  },
  {
    data: 'Положить асфальт',
    numObject: '1',
    deadline: '04.01.2026',
    status: 'Не выполнено'
  },
  {
    data: 'Положить асфальт',
    numObject: '1',
    deadline: '05.01.2026',
    status: 'Не выполнено'
  },
];
</script>

<style scoped lang="sass">

</style>
