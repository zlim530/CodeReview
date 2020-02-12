
public class SevenComparisonSort{
	public static void main(String[] args) {
		System.out.println("");
	}

	public static int cmp(int v1, int v2) {
		if ( v1 > v2) {
			return 1;
		} else if (v1 < v2) {
			return -1;
		} else{
			return 0;
		}
	}

	public static void swap(int k1, int k2) {
		int tmp = k2;
		k2 = k1;
		k1 = k2;
	}

	public static void BubbleSort(int[] array) {
		for (int end = array.length - 1; end > 0 ; end --) {
			for (int begin = 1; begin <= end; begin ++) {
				if ( cmp(array[begin-1],array[begin]) > 0) {
					swap(array[begin-1],array[begin]);
				}
			}
		}
	}

	public static void SelectionSort(int[] array) {
		for (int end = array.length - 1; end > 0; end --) {
			int maxIndex = 0;
			for (int begin = 1; begin <= end; begin++) {
				if ( cmp(array[begin],array[maxIndex]) >= 0 ) {
					maxIndex = begin;
				}
			}
			swap(array[maxIndex],array[array.length - 1]);
		}
	}

	public static void InsertionSort(int[] array) {
		for (int begin = 1; begin < array.length; begin++) {
			int cur = begin;
			while( cur>0 && cmp(array[begin],array[begin - 1]) < 0 ){
				swap(array[begin],array[begin-1]);
				// 因为一次循环中可能会存在多次比较交换才能让新插入的数据处于合适的位置
				cur--;
			}
		}
	}

}



