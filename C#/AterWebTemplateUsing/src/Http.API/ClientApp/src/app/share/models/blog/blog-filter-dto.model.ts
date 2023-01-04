export interface BlogFilterDto {
  pageIndex: number;
  pageSize: number;
  /**
   * 排序
   */
  orderBy?:  | null;
  /**
   * ����
   */
  title?: string | null;
  /**
   * ��Ҫ
   */
  summary?: string | null;
  /**
   * ����
   */
  content?: string | null;
  systemUserId?: string | null;

}
