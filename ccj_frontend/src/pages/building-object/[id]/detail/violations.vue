<template>
	<DefaultLayout>
		<BaseContainer>
			<h1 class="font-weight-bold mb-8">Нарушения</h1>
			<v-row justify="space-between">
				<v-col cols="12" sm="12" md="12" lg="12" class="mb-8">
					<v-data-table class="rounded-sm" :headers="Сonstants.headersViolations" :items="checkListData"
						:items-per-page="10" item-value="product" hover>
						<template v-slot:item.workStoppage="{ value }">
							<v-chip v-if="value" class="rounded-sm" color="error" close>Остановка работы</v-chip>
						</template>
						<template v-slot:item.commitData="{ value }">
							<v-btn :to="value" variant="tonal" color="info" size="small">Добавить</v-btn>
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
		category: 'Категория 1',
		id: '1',
		view: 'Вид',
		type: 'Тип',
		name: 'Название',
		deadline: '01.01.2026',
		dateReview: '15.11.2025',
		workStoppage: false,
		addition: 'Дополнение',
		commitData: '/'
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
		commitData: '/'
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
		commitData: '/'
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
		commitData: '/'
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
		commitData: '/'
	},
];
</script>

<style scoped lang="sass">

</style>
