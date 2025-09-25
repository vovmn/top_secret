<template>
	<DefaultLayout>
		<BaseContainer>
			<v-sheet class="mb-8 d-flex align-center ga-2" color="background">
				<h1 class="font-weight-bold">Объект #{{ id }}</h1>
				<v-chip outline :color="activeStatuses[status].color">{{ activeStatuses[status].name }}</v-chip>
			</v-sheet>

			<h2 class="mb-4 text-grey-lighten-1">Справка по объекту</h2>
			<v-row justify="space-between">
				<v-col cols="12" sm="12" md="7" lg="7">
					<v-table striped="even" class="rounded-sm border">
						<tbody>
							<tr v-for="(row, key) in Сonstants.headersBuildingObject" :key="key">
								<td :style="`background-color: ${colors.secondary}`" class="text-white">
									{{ row.header }}
								</td>
								<td v-if="row.actions === 'link'">
									<v-btn :to="`${route.path}/detail/${row.key}`" variant="text" size="small" color="info">{{ tableData[row.key] }}</v-btn>
								</td>
								<td v-else class="pl-7">
									{{ tableData[row.key] }}
								</td>
							</tr>
						</tbody>
					</v-table>
				</v-col>
				<v-col cols="12" sm="12" md="4" lg="4">
					<BuildingObjectContact class="mb-8" v-for="contact in contacts" :title="contact.title"
						:subtitle="contact.subtitle" :src="contact.src" />
				</v-col>

				<v-col cols="12" sm="12" md="7" lg="7">
					<h2 class="mb-4 text-grey-lighten-1">Товарно-транспортная накладная</h2>
					<v-data-table class="rounded-sm" :headers="Сonstants.headersTnn" :items="tnnData" :items-per-page="10"
						item-value="product" hover>
						<template v-slot:item.passport="{ value }">
							<v-btn :to="value" variant="tonal" color="info" size="small">Открыть</v-btn>
						</template>
					</v-data-table>
				</v-col>
				<v-col cols="12" sm="12" md="4" lg="4">
					<h2 class="mb-4 text-grey-lighten-1">Сводная панель</h2>
					<v-card>
						<v-card-text>
							<v-row>
								<v-col cols="12" sm="12" md="6" lg="6">
									<BuildingObjectDahboardCard
										:title="violationsLength > 0 ? `Нарушений: ${violationsLength}` : 'Нарушений нет'"
										:color="violationsLength > 0 ? 'warning' : 'info'"
										:container-class="violationsLength > 0 ? 'bg-warning d-flex flex-column' : 'bg-info d-flex flex-column'"
										:link="`/building-object/${id}/detail/violations`" />
								</v-col>
								<v-col cols="12" sm="12" md="6" lg="6">
									<BuildingObjectDahboardCard title="Сроки:" text="2 задачи заканчиваются завтра" color="info"
										:link="`/building-object/${id}/detail/schedule`" />
								</v-col>
								<v-col cols="12" sm="12" md="12" lg="12">
									<BuildingObjectDahboardCard title="Последняя проверка" text="03.04.2025 — требуется доработка"
										color="grey-darken-3" :link="`/building-object/${id}/detail/checklists`" />
								</v-col>
							</v-row>
						</v-card-text>
					</v-card>
				</v-col>
			</v-row>
		</BaseContainer>
	</DefaultLayout>
</template>

<script lang="ts" setup>
import { useTheme } from 'vuetify';

import * as Сonstants from '@/components/features/building_object/constants/index';
import { activeStatuses, type StatusKey } from '@/components/shared/constants/statuses';
import DefaultLayout from '@/layouts/DefaultLayout.vue';
import { VBtn } from 'vuetify/components';

const theme = useTheme();
const themeColors = theme.current.value.colors;
const colors = computed(() => themeColors);

const route = useRoute('/building-object/[id]/');
const id = route.params.id;

const status: StatusKey = 'active';

const tableData = {
	name: 'Устройство фундамента',
	status: status,
	poligon: 'Полигон',
	schedule: 'Перейти',
	violations: 'Перейти',
	chackListHistory: 'Перейти',
	materials: 'Перейти',
}

const contacts = [
	{
		title: "Иван Иванов",
		subtitle: "Заказчик",
		src: "https://cdn.vuetifyjs.com/images/john.png"
	},
	{
		title: "Петр Петров",
		subtitle: "Инспектор",
		src: "https://cdn.vuetifyjs.com/images/john.png"
	},
] satisfies { title: string, subtitle: string, src: string }[];

const tnnData = [
	{ product: 'Бетон М300', number: '1', volume: 45.5, unit: 'м³', date: '2025-04-01', passport: '/' },
	{ product: 'Бетон М301', number: '1', volume: 45.5, unit: 'м³', date: '2025-04-01', passport: '/' },
	{ product: 'Бетон М302', number: '1', volume: 45.5, unit: 'м³', date: '2025-04-01', passport: '/' },
	{ product: 'Бетон М303', number: '1', volume: 45.5, unit: 'м³', date: '2025-04-01', passport: '/' },
	{ product: 'Бетон М304', number: '1', volume: 45.5, unit: 'м³', date: '2025-04-01', passport: '/' },
	{ product: 'Бетон М305', number: '1', volume: 45.5, unit: 'м³', date: '2025-04-01', passport: '/' },
	{ product: 'Бетон М306', number: '1', volume: 45.5, unit: 'м³', date: '2025-04-01', passport: '/' },
	{ product: 'Бетон М307', number: '1', volume: 45.5, unit: 'м³', date: '2025-04-01', passport: '/' },
	{ product: 'Бетон М308', number: '1', volume: 45.5, unit: 'м³', date: '2025-04-01', passport: '/' },
	{ product: 'Бетон М309', number: '1', volume: 45.5, unit: 'м³', date: '2025-04-01', passport: '/' },
	{ product: 'Бетон М310', number: '1', volume: 45.5, unit: 'м³', date: '2025-04-01', passport: '/' },
	{ product: 'Бетон М311', number: '1', volume: 45.5, unit: 'м³', date: '2025-04-01', passport: '/' },
	{ product: 'Бетон М312', number: '1', volume: 45.5, unit: 'м³', date: '2025-04-01', passport: '/' },
];

const violationsLength = 3;
</script>